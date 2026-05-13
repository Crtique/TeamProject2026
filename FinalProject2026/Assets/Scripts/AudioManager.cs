// This is a Script writen by CJ Robinson. It is a signleton AudioManager
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    // Singleton Instance
    public static AudioManager Instance { get; private set; }

    private readonly List<AudioSource> sources = new List<AudioSource>();

    // Check if there is only on instance of this object and if there is not then destroy the other instance of itself.
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    // ---- Play sound effects and set the volume of them ---- //
    public AudioSource PlayAudio(AudioClip clip, float volume)
    {
        // Make a new GO and optionally place it in 3d space for where we want it to project from
        var newGO = new GameObject($"AudioSource_{clip.name}");

        // Set up the clip component
        var newSource = newGO.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.volume = volume;
        newSource.playOnAwake = false;

        sources.Add(newSource);
        newSource.Play();

        StartCoroutine(DestroyAudioSource(newSource));
        return newSource;
    }

    // ---- The Music Player ---- // 
    public AudioSource PlayMusic(AudioClip clip, float volume)
    {
        // Make a new GO and optionally place it in 3d space for where we want it to project from
        var newGO = new GameObject($"AudioSource_{clip.name}");

        // Set up the clip component
        var musicSource = newGO.AddComponent<AudioSource>();
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.playOnAwake = false;

        sources.Add(musicSource);
        musicSource.Play();
        musicSource.loop = true;

        StartCoroutine(DestroyAudioSource(musicSource));
        return musicSource;
    }

    private IEnumerator DestroyAudioSource(AudioSource desiredSource)
    {
        yield return new WaitWhile(() =>
        {
            return desiredSource != null && desiredSource.isPlaying;
        });

        if (desiredSource != null)
        {
            StopAudio(desiredSource);
        }
    }

    public void StopAudio(AudioSource desiredSource)
    {
        if (desiredSource == null)
        {
            return;
        }

        if (sources.Contains(desiredSource))
        {
            sources.Remove(desiredSource);
        }

        desiredSource.Stop();
        Destroy(desiredSource.gameObject);
    }
}
