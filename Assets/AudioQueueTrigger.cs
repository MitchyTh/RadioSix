using Unity.VisualScripting;
using UnityEngine;

public class AudioQueueTrigger : MonoBehaviour
{
    public AudioSource audioSource;

    public bool oneNDone = false;

    private bool flag = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(flag)
            return;
        Debug.Log("Enters area");
        if (other.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                if (oneNDone)
                    flag = true;
            }
        }
        
    }
}
