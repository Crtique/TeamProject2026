/*created by Autumn*/
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class EndLevelDoor : MonoBehaviour
{
    private bool doorActive = false;
    private bool loading = false;
    public string menu = "MainMenuObj";
    public string credits = "CreditsMenuObj";
    public string canvas = "Canvas";
    public AudioClip doorOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //loads the menu scene when the player presses E
        if (doorActive && Input.GetKeyDown(KeyCode.E)) {
            SceneManager.LoadScene(0); 
            loading = true;
            AudioManager.Instance.PlayAudio(doorOpen, 1f);
        }
    }

    private void LateUpdate()
    {
        if (loading) {
            GameObject.Find(menu).SetActive(false); //sets the menu to inactive
            GameObject.Find(canvas).transform.Find(credits).gameObject.SetActive(true); //sets the credits to active
            loading = false;
            Destroy(this.gameObject);//destroys the door
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            doorActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            doorActive = false;
        }
    }
}
