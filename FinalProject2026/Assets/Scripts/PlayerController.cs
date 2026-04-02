/*Script was written by CJ Robinson. handle player movement using ridgidbody*/
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- Declare Variables ---
    [Header("Player Movement")]
    public float speed = 10f;

    // --- Declare Components ---
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    // --- Player Movement Function ---
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-Vector3.right * speed * 10f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * speed * 10f, ForceMode.Force);
        }
    }

    // --- Jump Function ---
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
        }
    }
}
