using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]float Angular_Velocity = 300.0f;
    [SerializeField]float updates = 10;
    Rigidbody rb;
    void Awake(){
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate(){
        for(int i = 0; i < updates; i++){
            rb.AddTorque(Vector3.up*(Angular_Velocity - rb.angularVelocity.y)*rb.mass);
        }
    }
}
