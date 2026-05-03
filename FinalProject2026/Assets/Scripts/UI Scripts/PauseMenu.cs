using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas pauseScreen;

    bool paused = false;

    private void Start()
    {
        pauseScreen.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeGame();
                paused = false;
            }
            else
            {
                PauseGame();
                paused = true;
            }
        }
    }

    // Continue playig the game and unfree time
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseScreen.enabled = false;
    }

    // back to main screen
    public void QuitToTitle()
    {
        SceneManager.LoadScene("Menu");
    }

    // Exit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // open the pause menu
    void PauseGame()
    {
        pauseScreen.enabled = true;
        Time.timeScale = 0f;
    }
}
