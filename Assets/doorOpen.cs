using UnityEngine;
using UnityEngine.InputSystem;

public class DoorOpen : MonoBehaviour
{
    public float rotationAngle = 90f;        // Degrees to rotate
    public float rotationSpeed = 90f;        // Degrees per second
    public KeyBools keybools;                // Reference to KeyBools (make sure this is set)
    private Quaternion targetRotation;
    private bool isOpening = false;

    // Reference to the Input Action (assigned in the inspector)
    public InputAction openDoorAction;

    void Start()
    {
        // Set the target rotation to (0, 90, 0)
        targetRotation = Quaternion.Euler(0f, 90f, 0f);
        
        // Enable the action on start
        openDoorAction.Enable();
    }

    // Method that gets triggered by the Input Action (like pressing a button)
    public void OpenDoor()
    {
        // Check if the player can open the door
        if (keybools.canLeave && !isOpening)
        {
            isOpening = true; // Start opening the door
            StartCoroutine(RotateDoor()); // Start rotating the door smoothly
        }
    }

    private System.Collections.IEnumerator RotateDoor()
    {
        // Smoothly rotate towards the target rotation
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Ensure we set the exact target rotation once it's "close enough"
        transform.rotation = targetRotation;
        isOpening = false; // Door is fully open
    }

    // Cleanup: Disable the input action when not needed
    private void OnDisable()
    {
        openDoorAction.Disable();
    }
}
