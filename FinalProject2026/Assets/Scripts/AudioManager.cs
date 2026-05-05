using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Audio Sorces
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource music;

    // Singleton Instance
    public static AudioManager Instance { get; private set; }

    // Check if there is only on instance of this object and if there is not then destroy the other instance of itself.
    private void Awake()
    {
        if (Instance == null && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Continue over scenes
        DontDestroyOnLoad(this);

        // Perminetly assign the AudioSource Component
        music = GetComponent<AudioSource>();
        sfx = GetComponent<AudioSource>();
    }

    // Play sound effects and set the volume of them
    public void PlayAudio(AudioClip clip, float volume)
    {
        sfx.clip = clip;
        sfx.volume = volume;
        sfx.Play();
    }

    // Play gmae Music and set the volume of it
    public void PlayMusic(AudioClip clip, float volume)
    {
        music.clip = clip;
        music.volume = volume;
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }
}
