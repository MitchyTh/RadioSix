 using UnityEngine;

public class LightDetection : MonoBehaviour
{
    [Range(0f, 1f)] public float LightLevel = 0f;
    private Vector3 position;
    private bool flashlightOn = false;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        // AAA
    }
}
