/*Autumn made this one. */
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPOS;
    public Vector3[] waypoints;
    public float speed = 1f;
    public int pointer = 0;
    void Start()
    {
        //startPOS = transform.position;
        transform.position = waypoints[0];
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[pointer], speed * Time.deltaTime);
        if (transform.position == waypoints[pointer]) {
            updateWaypoint();
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
}
