/*Script was written by CJ Robinson. handle player platform detection*/
using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    [Header("Platform Control")]
    private RaycastHit platformHit;
    private bool exitPlatform;
    public float playerHeight = 2.5f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnPlatform() && !exitPlatform)
        {
            if (rb.linearVelocity.y < 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
    }

    // Detection for when we are on a Moving Platform
    private bool OnPlatform()
    {
        // Check when we are on the platform on not
        if (Physics.Raycast(transform.position, Vector3.down, out platformHit, playerHeight * 0.5f + 0.3f))
        {
            // Check the distance to the platform
            float distance = Vector3.Distance(transform.position, platformHit.normal);

            // Check if we are on a platform
            return distance < 0;
        }

        return false;
    }
}
