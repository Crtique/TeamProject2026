/*created by Autumn. Sets the player's checkpoint when they pass through a checkpoint's trigger, 
 also moves the player to the last touched checkpoint when they hit a killbox
 the checkpoint and killbox need the "Respawn" and "KillBox" tags respectively*/

using UnityEngine;
using UnityEngine.UIElements;

public class Respawn : MonoBehaviour
{
    public GameObject RespawnPoint;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(RespawnPoint != null && Input.GetKeyDown(KeyCode.R)) {
            rb.position = RespawnPoint.transform.position;
            rb.linearVelocity = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider Other)
    {
        if(Other.gameObject.tag == "Respawn") {
            RespawnPoint = Other.gameObject;
        }
        if(Other.gameObject.tag == "KillBox") {
            KillPLayer();
        }
    }

    private void KillPLayer()
    {
        rb.position = RespawnPoint.transform.position;
        rb.linearVelocity = new Vector3(0, 0, 0);
    }
}
