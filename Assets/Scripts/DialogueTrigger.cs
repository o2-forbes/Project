using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Transform NPC;
    [SerializeField] private bool isAfterTrigger = false;
    [SerializeField] private bool triggerOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (!triggerOnce || !hasTriggered))
        {
            dialogueManager.DialogueStart(GetDialogueLines(), NPC, isAfterTrigger);
            hasTriggered = true;
        }
    }

    private List<DialogueString> GetDialogueLines()
    {
  
        return new List<DialogueString>();
    }
}
