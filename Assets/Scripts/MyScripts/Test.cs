using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;
    [SerializeField] float force = 0;
    [SerializeField] uint updates = 50;
    [SerializeField] int multiplier = 1;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < updates; i++)
        {
            rb.AddForce(Vector3.up * force * rb.mass * multiplier, ForceMode.Impulse);
        }
    }
}
