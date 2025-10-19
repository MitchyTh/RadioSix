using UnityEngine;
using UnityEngine.UI;

public class ECGScroller : MonoBehaviour
{
    [Header("References")]
    public RawImage ecgImage;


    [Header("Settings")]
    //[Range(0f, 1f)]
    //public float dangerLevel = 0f;
    public GameObject playerDangerLevel;
    public float minScrollSpeed = 0.1f;
    public float maxScrollSpeed = 1.0f;

    private float currentOffset = 0f;
    private float previousOffset = 0f;

void Update()
{
    if (ecgImage == null) return;

    float stressLevel = playerDangerLevel.GetComponent<LightDetection>().stressLevel;
    Debug.Log("test");

    float scrollSpeed = Mathf.Lerp(minScrollSpeed, maxScrollSpeed, stressLevel);
    
    previousOffset = currentOffset;
    currentOffset += scrollSpeed * Time.deltaTime;
    currentOffset %= 1f;

    if (previousOffset > currentOffset)
    {
        Debug.Log("beep");
    }

    Rect uv = ecgImage.uvRect;
    uv.x = currentOffset;
    ecgImage.uvRect = uv;
}

}
