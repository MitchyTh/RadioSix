using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour
{
    public bool hasFlashlight = false;
    public GameObject model;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasFlashlight = false;
    }
    public void flashlight()
    {
        if(!hasFlashlight)
            return;
        Light mySpotlight = GetComponent<Light>();
        if (mySpotlight.enabled)
            mySpotlight.enabled = false;
        else
            mySpotlight.enabled = true;
    }
    public void pickup()
    {
        Light mySpotlight = GetComponent<Light>();
        hasFlashlight = true;
        model.SetActive(false);
        mySpotlight.enabled = true;
    }
}
