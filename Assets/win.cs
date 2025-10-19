using UnityEngine;

public class win : MonoBehaviour
{
    public GameObject winscreen;
    public GameObject pauseManager;
    private void OnTriggerEnter(Collider other)
    {
        pauseManager.GetComponent<PauseScreenManager>().SetPauseGame();
        winscreen.SetActive(true);
      
    }
}
