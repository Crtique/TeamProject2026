using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public bool doorActive = false;
    private bool doorEnabled = true;
    public GameObject exit;
    private GameObject player;
    private Rigidbody rb;

    void Update()
    {
        if (doorActive && Input.GetKeyDown(KeyCode.E) && doorEnabled && exit!=null) {
            doorEnabled = false;
            StartCoroutine(enterDoor());
        }
    }

    public IEnumerator enterDoor()
    {
        yield return new WaitForSeconds(1);
        doorEnabled = true;
        rb.position = exit.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            doorActive = true;
            player = other.gameObject;
            rb = player.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            doorActive = false;
        }
    }
}
