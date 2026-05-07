using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Audio Sorces
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource sfx2;
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
    }

    // Play sound effects and set the volume of them
    public void PlayAudio(AudioClip clip, float volume)
    {
        // Audio Clip
        sfx.clip = clip;
        
        // Volume of the sound effect
        sfx.volume = volume;

        // Play the sound
        sfx.Play();

        // Loop the audio
        sfx.loop = true;
    }

    // Play sound effects and set the volume of them
    public void PlayAudio2(AudioClip clip, float volume)
    {
        // Audio Clip
        sfx2.clip = clip;

        // Volume of the sound effect
        sfx2.volume = volume;

        // Play the sound
        sfx2.Play();

        // Loop the audio
        sfx2.loop = true;
    }

    // Play gmae Music and set the volume of it
    public void PlayMusic(AudioClip clip, float volume)
    {
        music.clip = clip;
        music.volume = volume;
        music.Play();
    }

    public void Stop(AudioClip clip)
    {
        sfx2.Stop();
    }
}
