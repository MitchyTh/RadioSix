using UnityEngine;
using UnityEngine.InputSystem;

public class DoorOpen : MonoBehaviour
{
    public float rotationAngle = 90f;       // Total angle to rotate
    public float rotationSpeed = 180f;      // Degrees per second
    public KeyBools keybools;
    private bool hasOpened = false;
    private bool isRotating = false;
    private bool inRange = false;

    public void RotateOnPivot()
    {
        if (!isRotating && inRange && !hasOpened && keybools.canLeave)
        {
            StartCoroutine(RotateSmoothly());
            hasOpened = true;
        }
    }

    private System.Collections.IEnumerator RotateSmoothly()
    {
        isRotating = true;

        float rotated = 0f;

        while (rotated < rotationAngle)
        {
            float step = rotationSpeed * Time.deltaTime;
            float rotateThisFrame = Mathf.Min(step, rotationAngle - rotated);

            transform.Rotate(0f, rotateThisFrame, 0f, Space.Self);
            rotated += rotateThisFrame;

            yield return null;
        }

        isRotating = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}

