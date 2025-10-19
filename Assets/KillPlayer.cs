using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public GameObject deathPanel;
    public GameObject pauseManager;
    void Start()
    {

    }

    // Update is called once per frame
    public void kill()
    {
        print("u died!!!");
        pauseManager.GetComponent<PauseScreenManager>().SetPauseGame();
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
