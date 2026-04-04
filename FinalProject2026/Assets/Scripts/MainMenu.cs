using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start Game
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Exit Game
    public void QuitGame()
    {
        Debug.Log("PLAYER HAS QUIT THE GAME!: Quit Game!");
        Application.Quit();
    }
}
