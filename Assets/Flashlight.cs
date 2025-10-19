using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class Flashlight : MonoBehaviour
{
    public bool hasFlashlight = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasFlashlight = true;


    }

    public void flashlight()
    {

        Light mySpotlight = GetComponent<Light>();

        if (mySpotlight.enabled)
        {
            mySpotlight.enabled = false;
        }
        else
        {
            mySpotlight.enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
