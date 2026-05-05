using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip Music;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic(Music, 0.4f);
    }
}
