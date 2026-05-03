/*made by Autumn*/
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerController player;
    [SerializeField] Animator anim;

    private bool AtLadder = false;
    private GameObject Ladder;
    private bool isClimbing = false;

    [Header("Ladder Jump")]
    [SerializeField] float LadderJumpUpwardForce;

    [Header("climbing")]
    [SerializeField] float speed;


    // -- Animation Bool --
    bool climbing;

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
        // Set animation
        anim.SetBool("climbing", climbing);

        if (AtLadder && !isClimbing && Input.GetKeyDown(KeyCode.W)) {//checks if the player wants to get on the ladder
            StartClimb();
        } else if(isClimbing && Input.GetKey(KeyCode.W)) {//checks if the player wants to move up the ladder
            // Start Climbing Animation
            climbing = true;
            ClimbUp();
        } else if(isClimbing && Input.GetKeyDown(KeyCode.Space)) { //checks if the player wants to jump off the ladder
            ClimbJump();
        }
    }

    //sets variables for the ladder climbing state to work
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
        LeaveLadder();

        // Multiply the jumpforce to move the player up on the Y axis 
        Vector3 forceToAdd = Vector3.up * LadderJumpUpwardForce;

        // Reset the players velocity
        rb.linearVelocity = Vector3.zero;

        // Add the Force so that the player jump upward while on a ladder
        rb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    //moves the player up the ladder
    private void ClimbUp()
    {
        rb.transform.position = new Vector3(rb.transform.position.x,rb.transform.position.y + Time.deltaTime * speed,rb.transform.position.z);
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
            LeaveLadder();
        }
    }

    //called when the player exits climbing the ladder, setting variables for normal gameplay
    private void LeaveLadder()
    {
        // End climbing Animation
        climbing = false;

        isClimbing = false;
        rb.useGravity = true;
        player.unlimited = false;
        player.restricted = false;
    }
}
