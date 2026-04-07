/*autumn made this one*/
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody playerRB;
    public Vector3 offset = new Vector3(0,2,-10f);
    public Vector3 velocity = new Vector3(0,0,0);
    public float speed = .2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(playerRB.transform.position.x, playerRB.transform.position.y + offset.y, offset.z);
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playerRB.transform.position.x, playerRB.transform.position.y + offset.y, offset.z), ref velocity, speed);
        if(velocity.magnitude < 0.0001) {
            velocity = new Vector3(0,0,0);
        } 
    }


}
