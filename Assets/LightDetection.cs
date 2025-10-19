using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    [Range(0f, 1f)] public float LightLevel = 0f;
    public GameObject staticEffectImage;
    private Vector3 position;
    private float lastBrightTime;
    private float brightThreshold = 0.5f;
    private float darkThreshold = 0.1f;
    private float chaseTimeout = 5.0f;
    private float max = 15.0f;
    private StaticScript staticScript;
    public GameObject flashlight;
    public bool hasPassedMonsterStart = false;
    public Light[] lights;

    private void Start()
    {
        position = transform.position;
        hasPassedMonsterStart = false;
        staticScript = staticEffectImage.GetComponent<StaticScript>();
        brightThreshold = 0.5f;
        darkThreshold = 0.1f;
    }

    private void Update()
    {
        LightLevel = getPlayerLight();
        if (LightLevel > brightThreshold)
        {
            lastBrightTime = Time.time;
            if (hasPassedMonsterStart)
            {
                //CALL START CHASE
            }
        }
        else if (LightLevel < darkThreshold && Time.time - lastBrightTime > chaseTimeout)
        {
            //CALL STOP CHASE
        }
        staticScript.setStatic(LightLevel, darkThreshold);
    }
    
    private float getPlayerLight()
    {
        position = transform.position;
        if (flashlight.GetComponent<Light>().enabled)
            return 1.0f;
        float totalIntensity = 0.0f;
        foreach (var light in lights)
            totalIntensity += IsPlayerHitByLight(light);
        return Math.Min(totalIntensity, max)/max;
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
            //Debug.DrawRay(position,lightPosition,Color.yellow);
            // If the ray hits something, check if it's the light source itself.
            // If it's something else, the player is blocked.
            if (hit.transform.gameObject != light.gameObject)
                return 0.0f;
        }
        float distance = Vector3.Distance(position, lightPosition);
        float exposureIntensity = light.intensity / Mathf.Pow(distance, 2);
        return exposureIntensity;
    }
}
