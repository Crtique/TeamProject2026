/*Script was written by CJ Robinson. handle UI menu*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip Select;
    [SerializeField] AudioClip Noise;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic(Noise, 1f);
    }

    // Start Game
    public void PlayGame()
    {
        StartCoroutine(DeleyLoad("SampleScene", 2f));
    }
    // Player UI Audio
    public void AudioPlayer()
    {
        AudioManager.Instance.PlayAudio(Select, 1f);
    }
    // Exit Game
    public void QuitGame()
    {
        Debug.Log("PLAYER HAS QUIT THE GAME!: Quit Game!");
        Application.Quit();
    }

    IEnumerator DeleyLoad(string name, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("SampleScene");
    }
}
