using UnityEngine;
using System;
using System.Collections.Generic;

public class LightDetection : MonoBehaviour
{
    [Range(0f, 1f)] public float LightLevel = 0f;

    private Vector3 position;
    private bool flashlightOn = false;

    public Light[] lights;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        foreach (var light in lights)
        {
            if (GetLightIntensity(light))
            {
                Debug.Log("Not hit by light.");
            }
            else
            {
                Debug.Log("Hit by light.");
            }
        }
    }
    
    bool GetLightIntensity(Light light)
    {
        RaycastHit hit;
        Vector3 lightPosition = light.transform.position;
        Physics.Raycast(transform.position, lightPosition, out hit, Mathf.Infinity);
        Debug.DrawRay(transform.position, lightPosition * hit.distance, Color.yellow);
        return (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity));
    }
}
