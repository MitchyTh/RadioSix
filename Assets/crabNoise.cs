using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CrabNoise : MonoBehaviour
{
    public float triggerDistance = 5f;
    public float cooldownTime = 3f;

    private float lastPlayTime = -Mathf.Infinity;
    private AudioSource audioSource;
    private Transform playerTransform;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure it's tagged 'Player'.");
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= triggerDistance && Time.time - lastPlayTime >= cooldownTime)
        {
            // Set volume based on how close the player is (closer = louder)
            float volume = 1f - (distance / triggerDistance);
            audioSource.volume = Mathf.Clamp01(volume);

            audioSource.Play();
            lastPlayTime = Time.time;
        }
    }
}
