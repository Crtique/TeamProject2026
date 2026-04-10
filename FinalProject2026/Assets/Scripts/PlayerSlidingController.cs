using UnityEngine;

public class PlayerSlidingController : MonoBehaviour
{
    [Header("Refrances")]
    public Transform playerObj;

    // --- Components ---
    private Rigidbody rb;
    private PlayerController player;

    [Header("Sliding")]
    public float slideForce;
    public float maxSlideTime;
    private float slideTimer;

    public float slidingHeight;
    private float startingHeight;

    // --- Inputs ---
    private float horizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();

        startingHeight = playerObj.localScale.y; 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // If the key is down and the player is grounded then slide
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartSlide();
        if (Input.GetKeyUp(KeyCode.LeftControl) && player.sliding )
            StopSlide();

    }
    private void FixedUpdate()
    {
        if (player.sliding)
            SlidingMovement();
    }

    void StartSlide()
    {
        player.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slidingHeight, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    void SlidingMovement()
    {
        // Slinding direction based on the players Input
        Vector3 slidingDirection = Vector3.right * horizontal;
        // Normal Sliding
        if (!player.OnSlope() || rb.linearVelocity.y > -0.1f)
        {
            rb.AddForce(slidingDirection * slideForce, ForceMode.Force);
        }
        // Sliding down a slope
        else
        {
            rb.AddForce(player.GetSlopeDirection(slidingDirection) * slideForce, ForceMode.Force);
        }
    }

    void StopSlide()
    {
        player.sliding = false;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startingHeight, playerObj.localScale.z);
    }
}
