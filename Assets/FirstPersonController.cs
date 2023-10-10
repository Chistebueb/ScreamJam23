using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float characterHeight = 2.0f;
    public float characterRadius = 0.5f;
    public LayerMask layerMask;
    public Camera playerCamera;

    private Rigidbody rb;
    private Vector3 velocity;
    private bool isGrounded;
    private int maxBounces = 5;
    private float skinWidth = 0.015f;
    private float maxSlopeAngle = 55;
    public AudioClip[] stepSounds; 
    public float stepInterval = 0.5f;

    private AudioSource audioSource;
    private float stepTimer = 0f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCamera == null)
        {
            Debug.LogError("Attach a camera to the FirstPersonController script.");
        }
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Handle mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the camera vertically
        playerCamera.transform.Rotate(Vector3.left * mouseY);

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Handle movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        velocity = move * speed;

        // Calculate if player is grounded using a capsule check
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.up * (characterHeight - characterRadius * 2);
        isGrounded = Physics.CheckCapsule(start, end, characterRadius, layerMask);

        // Handle step sounds
        if (velocity.magnitude > 0) // If player is moving and is on the ground
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval) // Time to play next step sound
            {
                PlayStepSound();
                stepTimer = 0f; // Reset timer
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer if player is not moving
        }
    }

    void FixedUpdate()
    {
        // Handle collision and sliding
        Vector3 finalVelocity = CollideAndSlide(velocity, transform.position, 0, false, velocity);
        rb.velocity = finalVelocity;
    }

    private Vector3 CollideAndSlide(Vector3 vel, Vector3 pos, int depth, bool gravityPass, Vector3 velInit)
    {
        if (depth >= maxBounces)
        {
            return Vector3.zero;
        }

        float dist = vel.magnitude + skinWidth;

        RaycastHit hit;
        if (Physics.CapsuleCast(pos, pos + Vector3.up * characterHeight, characterRadius, vel.normalized, out hit, dist, layerMask))
        {
            Vector3 snapToSurface = vel.normalized * (hit.distance - skinWidth);
            Vector3 leftover = vel - snapToSurface;

            float angle = Vector3.Angle(Vector3.up, hit.normal);

            if (snapToSurface.magnitude <= skinWidth)
            {
                snapToSurface = Vector3.zero;
            }

            // normal ground / slope
            if (angle <= maxSlopeAngle)
            {
                if (gravityPass)
                {
                    return snapToSurface;
                }

                leftover = ProjectAndScale(leftover, hit.normal);
            }
            // wall or steep slope
            else
            {
                float scale = 1 - Vector3.Dot(
                    new Vector3(hit.normal.x, 0, hit.normal.z).normalized,
                    -new Vector3(velInit.x, 0, velInit.z).normalized
                );

                leftover = ProjectAndScale(leftover, hit.normal) * scale;
            }

            return snapToSurface + CollideAndSlide(leftover, pos + snapToSurface, depth + 1, gravityPass, velInit);
        }

        return vel;
    }

    private Vector3 ProjectAndScale(Vector3 vector, Vector3 onNormal)
    {
        var projected = Vector3.ProjectOnPlane(vector, onNormal);
        return projected; // Implement scaling if needed
    }

    private void PlayStepSound()
    {
        int index = Random.Range(0, stepSounds.Length); // Choose a random step sound
        audioSource.clip = stepSounds[index];
        audioSource.pitch = Random.Range(0.95f, 1.05f); // Slightly alter the pitch
        audioSource.Play();
        Debug.Log("a");
    }
}
