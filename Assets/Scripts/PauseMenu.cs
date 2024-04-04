using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedOption = Mathf.Max(0, selectedOption - 1);
                UpdateSelectedOption();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedOption = Mathf.Min(pauseMenu.transform.childCount - 1, selectedOption + 1);
                UpdateSelectedOption();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ActivateSelectedOption();
            }
        }
    }

    void UpdateSelectedOption()
    {
        for (int i = 0; i < pauseMenu.transform.childCount; i++)
        {
            var button = pauseMenu.transform.GetChild(i).GetComponent<Button>();
            if (button != null)
            {
                button.interactable = (i == selectedOption);
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        selectedOption = 0;
        UpdateSelectedOption();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ActivateSelectedOption()
    {
        var button = pauseMenu.transform.GetChild(selectedOption).GetComponent<Button>();
        if (button != null && button.interactable)
        {
            button.onClick.Invoke();
        }
    }
}
