using UnityEngine;
using TMPro;
using System.Collections;

public class PanelController : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject storyPanel;
    public TMP_Text storyText; // Use TMP_Text for TextMeshPro text component
    public AudioClip skipSoundEffect; // Sound effect to play when skipping panels
    [Range(0f, 1f)] public float soundVolume = 1f; // Volume of the sound effect

    private bool titlePanelActive = true;
    private bool storyPanelActive = false;

    public float fadeSpeed = 1f; // Adjust this value to control the fade-in speed

    private AudioSource audioSource;

    void Start()
    {
        // Initially, deactivate the storyPanel and set its text alpha to 0
        storyPanel.SetActive(false);
        Color textColor = storyText.color;
        textColor.a = 0f;
        storyText.color = textColor;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("AudioSource component is missing.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (titlePanelActive)
            {
                titlePanel.SetActive(false);
                titlePanelActive = false;
                StartCoroutine(FadeInText());

                // Play sound effect for skipping the title panel
                if (skipSoundEffect != null && audioSource != null)
                {
                    audioSource.volume = soundVolume;
                    audioSource.PlayOneShot(skipSoundEffect);
                }
            }
            else if (storyPanelActive)
            {
                storyPanel.SetActive(false);
                storyPanelActive = false;

                // Play sound effect for skipping the story panel
                if (skipSoundEffect != null && audioSource != null)
                {
                    audioSource.volume = soundVolume;
                    audioSource.PlayOneShot(skipSoundEffect);
                }
            }
        }
    }

    IEnumerator FadeInText()
    {
        // Activate the storyPanel
        storyPanel.SetActive(true);

        // Gradually increase the alpha value of the text to fade it in
        while (storyText.alpha < 1f)
        {
            Color textColor = storyText.color;
            textColor.a += Time.deltaTime * fadeSpeed; // Adjust fading speed
            storyText.color = textColor;
            yield return null;
        }

        storyPanelActive = true;
    }
}
