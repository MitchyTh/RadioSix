using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    [Range(0f, 1f)] public float LightLevel = 0f;

    private Vector3 position;
    public GameObject flashlight;
    public Light[] lights;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        print(getPlayerLight());
    }
    
    private float getPlayerLight()
    {
        float max = 100.0f;
        if (flashlight.GetComponent<Light>().enabled)
            return 1.0f;
        float totalIntensity = 0.0f;
        foreach (var light in lights)
            totalIntensity += IsPlayerHitByLight(light);
        return Math.Max(totalIntensity, max)/30.0f;
    }
    private float IsPlayerHitByLight(Light light)
    {
        Vector3 lightPosition = light.transform.position;
        Vector3 lightDirection = light.transform.forward;
        Vector3 lightToPlayerDirection = (position - lightPosition).normalized;
        if(light.type == LightType.Spot)
        {
            float dotProduct = Vector3.Dot(lightDirection, lightToPlayerDirection);
            float cosHalfSpotAngle = Mathf.Cos(light.spotAngle / 2f * Mathf.Deg2Rad);
            if (dotProduct < cosHalfSpotAngle)
                return 0.0f;
        }
        RaycastHit hit;
        Vector3 rayDirection = (lightPosition - position).normalized;
        float maxDistance = Vector3.Distance(position, lightPosition);
        if (Physics.Raycast(position, rayDirection, out hit, maxDistance))
        {
            // If the ray hits something, check if it's the light source itself.
            // If it's something else, the player is blocked.
            if (hit.transform.gameObject != light.gameObject)
            {
                return 0.0f;
            }
        }
        float distance = Vector3.Distance(position, lightPosition);
        float exposureIntensity = Mathf.Clamp(light.intensity / Mathf.Pow(distance, 2), 0, 1);
        Debug.Log($"Current Exposure: {exposureIntensity}");
        return exposureIntensity;
    }


    //float GetLightIntensity(Light light)
    //{
    //    RaycastHit hit;
    //    Vector3 lightPosition = light.transform.position;
    //    Vector3 lightDirection = light.transform.forward;
    //    Vector3 toOther = Vector3.Normalize(transform.position - light.transform.position);

    //    float exposureIntensity;
    //    if(Vector3.Dot(lightDirection, toOther) < 0)
    //    {
    //        Debug.Log($"{light.gameObject.name}: Behind light");
    //    } else
    //    {
    //        Debug.Log($"{light.gameObject.name}: In Light");
    //    }




    //    Physics.Raycast(transform.position, lightPosition, out hit, Mathf.Infinity);
    //    Debug.DrawRay(transform.position, lightPosition * hit.distance, Color.yellow);

        
    //    Debug.Log($"Light Direction for {light.gameObject.name}: {lightDirection}");

    //    Debug.DrawRay(light.transform.position, lightDirection * 10f, Color.red, 15f);

    //    // Currently grabs the distance, not accounting for the player being in front of the light.  
    //    exposureIntensity = Mathf.Clamp(light.intensity / Mathf.Pow(hit.distance, 2), 0,1);
        
    //    //Debug.Log($"Light Intensity {exposureIntensity}");
        
    //    return exposureIntensity;
    //    //return (Physics.Raycast(transform.position, lightPosition, out hit, Mathf.Infinity));
    //}
}
