using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBools : MonoBehaviour
{
    bool key1 = false, key2 = false, key3 = false, key4 = false;
    public bool canLeave = false;
    private bool flag = false;

    public bool flashlightInRange;
    public LightDetection lightDetection;
     private GameObject keyInRange; // Stores key the player is near
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void acquireKey1()
    {
        key1 = true;
        checkCanLeave();
        checkFirstKey();
        audioSource.Play();
    }

    public void acquireKey2()
    {
        key2 = true;
        checkCanLeave();
        checkFirstKey();
        audioSource.Play();
    }

    public void acquireKey3()
    {
        key3 = true;
        checkCanLeave();
        checkFirstKey();
        audioSource.Play();
    }

    public void acquireKey4()
    {
        key4 = true;
        checkCanLeave();
        checkFirstKey();
        audioSource.Play();
    }
    public void checkCanLeave()
    {
        if (key1 && key2 && key3 && key4)
        {
            canLeave = true;
            lightDetection.hasLastKey = true;
        }
    }

    public void checkFirstKey()
    {
        Debug.Log("Check first key");
        if (lightDetection.hasFirstKey == false && flag ==true)
        {
            print("set true");
            lightDetection.hasFirstKey = true;
        }

        flag = true;
    }

        public void tryToPickupKey(InputAction.CallbackContext context)
    {
        // Only run on key press (not release)
        if (!context.performed || keyInRange == null) return;

        string tag = keyInRange.tag;

        if (tag == "Key1")
        {
            acquireKey1();
        }
        else if (tag == "Key2")
        {
            acquireKey2();
        }
        else if (tag == "Key3")
        {
            acquireKey3();
        }
        else if (tag == "Key4")
        {
            acquireKey4();
        }

        // Destroy the key object after pickup
        Destroy(keyInRange);
        keyInRange = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key1") || other.CompareTag("Key2") || other.CompareTag("Key3") || other.CompareTag("Key4"))
        {
            keyInRange = other.gameObject;
            Debug.Log("Near a key: " + keyInRange.tag + " — Press [E] to pick up.");
        }
        if (other.CompareTag("Flashlight"))
        {
            flashlightInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == keyInRange)
        {
            keyInRange = null;
            Debug.Log("Left key pickup range.");
        }
        if (other.CompareTag("Flashlight"))
        {
            flashlightInRange = false;
        }
    }
}
