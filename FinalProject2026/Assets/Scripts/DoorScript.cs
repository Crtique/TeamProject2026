using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool doorActive = false;
    public GameObject exit;
    private GameObject player;

    void Update()
    {
        if(doorActive && Input.GetKeyDown(KeyCode.E)) {
            player.transform.position = exit.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            doorActive = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            doorActive = false;
        }
    }
}
