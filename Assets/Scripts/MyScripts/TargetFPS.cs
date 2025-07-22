using UnityEngine;

public class TargetFPS : MonoBehaviour
{
    [SerializeField]
    private int targetFrameRate = 60; // Set your desired frame rate here
    private void Start()
    {
        // Hide the cursor
        Cursor.visible = false;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}