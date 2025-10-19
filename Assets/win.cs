using UnityEngine;

public class win : MonoBehaviour
{
    public GameObject winscreen;
    private void OnTriggerEnter(Collider other)
    {
        winscreen.SetActive(true);
      
    }
}
