using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio Sorces
    [SerializeField] AudioSource effectSound;

    // Singleton Instance
    public static AudioManager Instance { get; private set; }

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
        DontDestroyOnLoad(this);
    }
}
