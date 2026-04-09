/*autumn created this script*/
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody playerRB;
    public Vector3 offset = new Vector3(-5, 2, -10f); //where the camera is in relation to the player
    public Vector3 velocity = new Vector3(0, 0, 0);
    public float speed = .2f;
    void Start()
    {
        transform.position = new Vector3(playerRB.transform.position.x + offset.x, playerRB.transform.position.y + offset.y, offset.z);

    }

    void LateUpdate()
    {
        //moves the camera to where the player is (with an offset) with dammpening
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playerRB.transform.position.x + offset.x, playerRB.transform.position.y + offset.y, offset.z), ref velocity, speed);
        //sets the camera's velocity to 0 when it has come to a stop but still has an extremely low velocity
        if (velocity.magnitude < 0.0001) {
            velocity = new Vector3(0, 0, 0);
        }
    }
}
