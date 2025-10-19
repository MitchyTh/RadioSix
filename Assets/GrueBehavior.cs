using UnityEngine;

public class GrueBehavior : MonoBehaviour
{

    public bool hasGrue = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasGrue = false;
    }
    public void DoGrue()
    {
        Debug.Log("GRUE FLEE!");
    }
}
