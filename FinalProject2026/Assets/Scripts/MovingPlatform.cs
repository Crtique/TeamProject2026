/*Autumn made this one. */
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] waypoints;
    public float speed = 1f;
    public int pointer = 0;
    public bool startMoveWhenPlayer = false; //set to true to have platform start moving only after the player steps on it 
    public GameObject player;
    public Vector3 oldPOS;
    void Start()
    {
        // sets the location of the platform to the first waypoint
        transform.localPosition = waypoints[0];
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!startMoveWhenPlayer) {
            oldPOS = transform.localPosition;
            // moves platform
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, waypoints[pointer], speed * Time.deltaTime);
            if (player != null) {
                //moves player how the platform moved
                player.transform.localPosition += Vector3.MoveTowards(oldPOS, waypoints[pointer], speed * Time.deltaTime) - oldPOS;
            }
            if (transform.localPosition == waypoints[pointer]) {
                // iterates through the waypoints list when the platform is at a waypoint
                updateWaypoint();
            }
        }
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

    // when the player hits the trigger set "player" 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            startMoveWhenPlayer = false;
            player = other.gameObject;
        }
    }
    
    //when the player leaves the trigger change "player" to null so there's nothing for the platform to move
    private void OnTriggerExit(Collider other)
    {
        player = null;
    }
}
