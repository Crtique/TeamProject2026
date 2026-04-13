/*Script was written by CJ Robinson. handle player movement using ridgidbody*/
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // --- Declare Variables ---
    [Header("Player Movement")]
    public float speed = 15f;
    private Vector3 move;

    public bool sliding;

    [Header("Jumping Control")]
    public float jumpHeight = 20f;
    public float jumpCooldown = 5f;
    public float airMultiplier = 3;
    private bool ableToJump;

    [Header("Ground Drag")]
    public float gDrag = 5f; // Ground Drag

    [Header("Ground Checks")]
    public float playerHeight;
    public LayerMask isGround;
    private bool isGrounded;

    [Header("Slope Control")]
    const float slopeAngle = 45f;
    private RaycastHit slopeHit;
    private bool exitSlop;

    // --- Declare Components ---
    private Rigidbody rb;

    void Awake()
    {
        // Grab the Ridgidbody Component at the start of the game
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Speed:" + rb.linearVelocity.magnitude);
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);
        JumpReset();

        Inputs();

        // Handle the drag
        if (isGrounded)
            rb.linearDamping = gDrag;
        else
            rb.linearDamping = 0;
    }

    // FixedUpdate is called every fixed frame
    private void FixedUpdate()
    {
        PlayerMove();
        SpeedControl();
    }

    void Inputs()
    {
        // Call Jump funtions
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ableToJump = false;
            Jump();
        }
    }

    // Player Movement Function called every fixed frame
    void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        move = transform.right * horizontal;

        if (OnSlope() && !exitSlop)
        {
            rb.AddForce(GetSlopeDirection(move) * speed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y < 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        if (isGrounded)
            rb.AddForce(move * speed * 10f, ForceMode.Force);

        // in air
        else
            rb.AddForce(move * speed * 10f * airMultiplier, ForceMode.Force);
    }

    // Player Speed Control function called every frame
    void SpeedControl()
    {
        // Exit the function when we are sliding
        if (sliding) return;

        // Limit slope speed
        else if (OnSlope() && !exitSlop)
        {
            if (rb.linearVelocity.magnitude > speed)
                rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
        // Limit speed on ground or in the air
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Limit the velocity if need be
            if (flatVel.magnitude > speed)
            {
                Vector3 limitedVel = flatVel.normalized * speed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }

    }

    // Jump Function called every frame
    void Jump()
    {
        exitSlop = true;

        // Reset the Y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
    void JumpReset()
    {
        if (!ableToJump && isGrounded)
        {
            ableToJump = true;
            exitSlop = false;
        }
    }

    // Slope Control
    public bool OnSlope()
    {
        // Check if we are on the slope and store that posisiton
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            // Calculate how steep the slope is with Vector3.Angle function
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            // Check if we are angled or not
            return angle < slopeAngle && angle != 0f;

        }

        return false;
    }

    // Get the direction we move on the slope
    public Vector3 GetSlopeDirection(Vector3 move)
    {
        return Vector3.ProjectOnPlane(move, slopeHit.normal).normalized;
    }
}