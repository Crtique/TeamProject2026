/*made by Autumn*/
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool doorActive = false;
    private bool doorEnabled = true;
    public GameObject exit;
    private GameObject player;
    private Rigidbody rb;
    private GameObject playerRender;
    private PlayerController controller;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    void Update()
    {
        if (doorActive && Input.GetKeyDown(KeyCode.E) && doorEnabled && exit!=null) {
            doorEnabled = false;
            playerRender.SetActive(false);
            controller.unlimited = true;
            controller.restricted = true;
            AudioManager.Instance.PlayAudio(doorOpen, 1f);
            StartCoroutine(enterDoor());
        }
    }

    public IEnumerator enterDoor()
    {
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlayAudio(doorClose, 1f);
        playerRender.SetActive(true);
        doorEnabled = true;
        controller.unlimited = false;
        controller.restricted = false;
        rb.position = exit.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            doorActive = true;
            player = other.gameObject;
            rb = player.GetComponent<Rigidbody>();
            controller = player.GetComponent<PlayerController>();
            playerRender = player.transform.GetChild(0).gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            doorActive = false;
        }
    }
}
