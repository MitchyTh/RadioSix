using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour
{
    public KeyBools keybools;
    private bool pickedUp = false;
    private bool playerInRange;
    public bool hasFlashlight = false;
    public GameObject model;

    public TextMeshPro text;
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
        {
            mySpotlight.enabled = false;
            text.enabled = false;   
        }
        else
            mySpotlight.enabled = true;
    }
    public void pickup()
    {
        if (!hasFlashlight && keybools.flashlightInRange == true)
        {
            Light mySpotlight = GetComponent<Light>();
            hasFlashlight = true;
            model.SetActive(false);
            mySpotlight.enabled = true;
            pickedUp = true;
            text.enabled = true;
        }
    }
    
}
