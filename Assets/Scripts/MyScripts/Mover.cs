
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float Player_Velocity = 100.0f;
    [SerializeField] float Player_Jump_Force = 100.0f;
    [SerializeField] float Player_After_Jump_Speed_Multiplier = 0.1f;
    [SerializeField] float threshold = 0.1f;
    private bool isGrounded;
    private bool ApplyMultiplier;
    [SerializeField] float groundCheckDistance = 0.5f;
    [SerializeField] float sphereRadius = 0.5f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit check;
        isGrounded = Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out check, groundCheckDistance);

        Custom_Controller();
        Debug.Log($"Velocity : {Mathf.Sqrt(rb.linearVelocity.x * rb.linearVelocity.x + rb.linearVelocity.z * rb.linearVelocity.z)}");
    }
    void Custom_Controller()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float root_2 = (float)Mathf.Sqrt(2);

        float Xdir = h * Player_Velocity;
        float Zdir = v * Player_Velocity;

        float Player_Diagonal_Velocity_X = Xdir / root_2;
        float Player_Diagonal_Velocity_Z = Zdir / root_2;

        if (isGrounded)
        {

            if (Mathf.Abs(h) > threshold && Mathf.Abs(v) > threshold)
            {
                rb.linearVelocity = new Vector3(Player_Diagonal_Velocity_X, rb.linearVelocity.y, Player_Diagonal_Velocity_Z);
            }
            else
            {
                rb.linearVelocity = new Vector3(Xdir, rb.linearVelocity.y, Zdir);
            }
            ApplyMultiplier = true;
        }
        else
        {
            if (ApplyMultiplier) // Ensure this runs only once
            {
                Vector3 currentVelocity = rb.linearVelocity;
                float multiplier = Player_After_Jump_Speed_Multiplier;

                // Apply multiplier to X and Z velocities
                rb.linearVelocity = new Vector3(currentVelocity.x * multiplier, currentVelocity.y, currentVelocity.z * multiplier);

                ApplyMultiplier = false; // Prevent further modifications
            }
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Player_Jump_Force, ForceMode.Impulse);
        }
    }


}