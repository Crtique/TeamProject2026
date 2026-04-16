/*Autumn made this one. */
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] waypoints;
    public float speed = 1f;
    public int pointer = 0;
    public bool startMoveWhenPlayer = false; //set to true to have platform start moving only after the player steps on it 
    void Start()
    {
        transform.localPosition = waypoints[0];
    }

    void Update()
    {
        if (!startMoveWhenPlayer) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, waypoints[pointer], speed * Time.deltaTime);
            if (transform.localPosition == waypoints[pointer]) {
                updateWaypoint();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void updateWaypoint()
    {
        pointer++;
        if(pointer >= waypoints.Length) {
            pointer = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player") {
            startMoveWhenPlayer = false;
        }
    }
}
