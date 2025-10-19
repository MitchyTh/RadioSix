using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public GameObject deathPanel;
    void Start()
    {

    }

    // Update is called once per frame
    public void kill()
    {
        print("u died!!!");
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
