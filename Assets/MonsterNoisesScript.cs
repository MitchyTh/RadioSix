using UnityEngine;
using UnityEngine.Rendering;

public class MonsterNoisesScript : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 20f; //Furthest distance you can hear monster music
    public float minDistance = 5f; //Minimum distance you can hear music
    public float maxVolume = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {
         float distance = Vector3.Distance(player.position, transform.position);

        // Only do something if within range
        if (distance <= maxDistance)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();

            // Map distance to volume (closer = louder)
            float t = Mathf.InverseLerp(maxDistance, minDistance, distance);
            float volume = Mathf.Lerp(0f, maxVolume, t);
            audioSource.volume = volume;
        }
        else
        {
            // Outside range, fade out or stop
            if (audioSource.isPlaying)
            {
                audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, Time.deltaTime);
                if (audioSource.volume <= 0.01f)
                    audioSource.Stop();
            }
        }
    }
}
