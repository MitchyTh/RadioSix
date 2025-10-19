using UnityEngine;

public class SpinNBounce : MonoBehaviour
{
    [Header("Spin Settings")]
    public float rotationSpeed = 90f; // Degrees per second

    [Header("Bounce Settings")]
    public float bounceHeight = 0.5f; // How high it moves up and down
    public float bounceSpeed = 2f;    // How fast it bounces

    private Vector3 startPos;

    void Start()
    {
        // Store the starting position so it can bounce relative to it
        startPos = transform.position;
    }

    void Update()
    {
        // --- Spin ---
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // --- Bounce ---
        float newY = startPos.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
