using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject fadeToBlackTrigger;
    [SerializeField] private GameObject endTrigger;

    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float turnSpeed = 2f;

    private List<DialogueString> dialogueList;

    [Header("Player")]
    [SerializeField] private StarterAssets.FirstPersonController firstPersonController;
    private Transform playerCamera;

    private int currentDialogueIndex = 0;
    private bool isAfterTriggerDialogue = false;

    [SerializeField] private Image blackPanel; 

    private void Start()
    {
        dialogueParent.SetActive(false);
        playerCamera = Camera.main.transform;

        if (blackPanel == null)
        {
            Debug.LogError("Black Panel Image not assigned!");
        }
    }

    public void DialogueStart(List<DialogueString> textToPrint, Transform NPC, bool isAfterTrigger = false)
    {
        isAfterTriggerDialogue = isAfterTrigger;

        dialogueParent.SetActive(true);
        firstPersonController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Camera.main.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;

        StartCoroutine(TurnCameraTowardsNPC(NPC));

        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        DisableButtons();

        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons()
    {
        option1Button.interactable = false;
        option2Button.interactable = false;

        option1Button.GetComponentInChildren<TMP_Text>().text = "No Option";
        option2Button.GetComponentInChildren<TMP_Text>().text = "No Option";
    }

    private IEnumerator TurnCameraTowardsNPC(Transform NPC)
    {
        Quaternion startRotation = playerCamera.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(NPC.position - playerCamera.position);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * turnSpeed;
            yield return null;
        }

        playerCamera.rotation = targetRotation;
    }

    private bool optionSelected = false;

    private IEnumerator PrintDialogue()
    {
        while (currentDialogueIndex < dialogueList.Count)
        {
            DialogueString line = dialogueList[currentDialogueIndex];

            line.startDialogueEvent?.Invoke();

            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                option1Button.interactable = true;
                option2Button.interactable = true;

                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;

                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }

            line.endDialogueEvent?.Invoke();

            optionSelected = false;
        }

        DialogueStop();
    }

    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();

        currentDialogueIndex = indexJump;
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (dialogueList[currentDialogueIndex].isEnd)
            DialogueStop();

        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);

        Camera.main.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;

        firstPersonController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (isAfterTriggerDialogue && fadeToBlackTrigger != null)
        {
            fadeToBlackTrigger.GetComponent<FadeToBlackTrigger>().StartFadeToBlack();
        }

        if (isAfterTriggerDialogue)
        {
            blackPanel.gameObject.SetActive(true);
            if (endTrigger != null)
            {
                endTrigger.SetActive(true);
            }
        }
    }
}
