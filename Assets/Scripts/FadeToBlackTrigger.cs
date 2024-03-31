using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlackTrigger : MonoBehaviour
{
    [SerializeField] private Image fadePanel; 
    [SerializeField] private GameObject endTrigger; 

    public void StartFadeToBlack()
    {
        StartCoroutine(FadeToBlackCoroutine(endTrigger));
    }

    private IEnumerator FadeToBlackCoroutine(GameObject endTrigger)
    {
        fadePanel.gameObject.SetActive(true);

        float fadeDuration = 1.0f;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            Color newColor = fadePanel.color;
            newColor.a = alpha;
            fadePanel.color = newColor;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Color finalColor = fadePanel.color;
        finalColor.a = 1f;
        fadePanel.color = finalColor;

        if (endTrigger != null)
        {
            endTrigger.SetActive(true);
        }
    }
}
