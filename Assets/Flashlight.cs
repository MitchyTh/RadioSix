using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public bool hasFlashlight = false;
public class Flashlight : MonoBehaviour
{
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
