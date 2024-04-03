using UnityEngine;
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
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedOption = Mathf.Min(1, selectedOption + 1);
            }

            // Highlight selected option
            // Assuming that the resume button is the first child and the quit button is the second child of the pauseMenu
            for (int i = 0; i < pauseMenu.transform.childCount; i++)
            {
                var button = pauseMenu.transform.GetChild(i).GetComponent<UnityEngine.UI.Button>();
                if (button != null)
                {
                    if (i == selectedOption)
                    {
                        button.Select();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ActivateSelectedOption();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
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
        switch (selectedOption)
        {
            case 0:
                ResumeGame();
                break;
            case 1:
                QuitGame();
                break;
            default:
                break;
        }
    }
}
