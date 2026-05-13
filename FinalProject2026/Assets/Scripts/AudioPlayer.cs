// This script was writen by CJ Robinson. It handles the ambient noise and game music.
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip Music;
    [SerializeField] AudioClip sound;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic(Music, 1f);
        AudioManager.Instance.PlayMusic(sound, 0.3f);
    }
}
