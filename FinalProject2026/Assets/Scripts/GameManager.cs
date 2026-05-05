using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    // -- Declare Variables
    [SerializeField] Canvas gameOver;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timeRemaining = 60; // this is in seconds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;    // Always set the time to be normal speed at the start of the game
        gameOver.enabled = false; // Alawyas disable the Game Over Screen
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0.0f)
        {
            timeRemaining -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);

            timerText.text = string.Format("Get to class: {0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timeRemaining = 0.0f;
            GameOver();
        }
    }

    // When the game is over freeze the game and bring up a screen.
    void GameOver()
    {
        Time.timeScale = 0f;

        timerText.enabled = false;
        gameOver.enabled = true;
        AudioManager.Instance.StopMusic();
    }
    
    // Restart the game
    public void Restart()
    {
        Debug.Log("Game Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
