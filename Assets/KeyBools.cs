using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBools : MonoBehaviour
{
    bool key1 = false, key2 = false, key3 = false, canLeave = false;
     private GameObject keyInRange; // Stores key the player is near

    public void acquireKey1()
    {
        key1 = true;
        checkCanLeave();
    }

    public void acquireKey2()
    {
        key2 = true;
        checkCanLeave();
    }

    public void acquireKey3()
    {
        key3 = true;
        checkCanLeave();
    }

    public void checkCanLeave()
    {
        if (key1 && key2 && key3)
        {
            canLeave = true;
            Debug.Log("You Can Leave");
        }
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

        // Destroy the key object after pickup
        Destroy(keyInRange);
        keyInRange = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key1") || other.CompareTag("Key2") || other.CompareTag("Key3"))
        {
            keyInRange = other.gameObject;
            Debug.Log("Near a key: " + keyInRange.tag + " — Press [E] to pick up.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == keyInRange)
        {
            keyInRange = null;
            Debug.Log("Left key pickup range.");
        }
    }
}
