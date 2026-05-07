using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip Music;
    [SerializeField] AudioClip sound;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic(Music, 1f);
        AudioManager.Instance.PlayAudio(sound, 1f);
    }
}
