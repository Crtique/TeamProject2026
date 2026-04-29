using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerController player;

    private bool AtLadder = false;
    private GameObject Ladder;
    private bool isClimbing = false;

    [Header("Ladder Jump")]
    public float LadderJumpUpwardForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(AtLadder && !isClimbing && Input.GetKeyDown(KeyCode.W)) {
            StartClimb();
        } else if(isClimbing && Input.GetKey(KeyCode.W)) {
            ClimbUp();
        } else if(isClimbing && Input.GetKeyDown(KeyCode.Space)) {
            ClimbJump();
        }
    }

    private void StartClimb()
    {
        rb.transform.position = new Vector3(Ladder.transform.position.x, rb.transform.position.y, rb.transform.position.z);
        isClimbing=true;

        player.unlimited = true;
        player.restricted = true;

        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
    }

    private void ClimbJump()
    {
        isClimbing = false;
        rb.useGravity = true;
        player.unlimited = false;
        player.restricted = false;

        // Multiply the jumpforce to move the player up on the Y axis 
        Vector3 forceToAdd = Vector3.up * LadderJumpUpwardForce;

        // Reset the players velocity
        rb.linearVelocity = Vector3.zero;

        // Add the Force so that the player jump upward while on a ladder
        rb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    private void ClimbUp()
    {
        rb.transform.position = new Vector3(rb.transform.position.x,rb.transform.position.y + Time.deltaTime,rb.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        //sets the ladder that the player is at
        if (other.tag == "Ladder") {
            AtLadder = true;
            Ladder = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder") {
            AtLadder = false;
            Ladder = null;
            isClimbing = false;
        }
    }
}
