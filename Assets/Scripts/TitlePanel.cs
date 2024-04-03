using UnityEngine;
using UnityEngine.UI;

public class BlackPanel : MonoBehaviour
{
    public GameObject panel;
    public Text text;

    void Start()
    {
        // Make sure the panel is initially active
        panel.SetActive(true);

        // Make sure the text is initially disabled
        text.enabled = false;
    }

    void Update()
    {
        // Check for spacebar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If spacebar is pressed, toggle the visibility of the panel and text
            panel.SetActive(false);
            text.enabled = !text.enabled;
        }
    }
}
