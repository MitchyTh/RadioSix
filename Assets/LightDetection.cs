using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    [Range(0f, 1f)] public float LightLevel = 0f;
    private Vector3 position;
    private float lastBrightTime;
    private float lastDarkTime;
    private float brightThreshold = 0.5f;
    private float chaseTimeout = 5.0f;
    private float chaseTimein = 3.0f;
    private float max = 15.0f;
    public GameObject flashlight;
    public bool hasFirstKey = false;
    public bool hasLastKey = false;
    public Light[] lights;
    public GameObject monster;
    public float killDistance = 1.7f;
    public float stressLevel;

    private void Start()
    {
        position = transform.position;
        hasFirstKey = false;
        hasLastKey = false;
        brightThreshold = 0.05f;
        stressLevel = 0;
        lastBrightTime = Time.time;
        lastDarkTime = Time.time;

    }

    private void Update()
    {
        LightLevel = getPlayerLight();
        if ((LightLevel > brightThreshold && Time.time - lastDarkTime > chaseTimein) || hasLastKey)
        {
            lastBrightTime = Time.time;
            if (hasFirstKey)
                monster.GetComponent<AgentFollowPlayer>().StartChase();
        }
        else if (LightLevel < brightThreshold && Time.time - lastBrightTime > chaseTimeout)
        {
            lastDarkTime = Time.time;
            if (!hasLastKey)
                monster.GetComponent<AgentFollowPlayer>().StopChase();
        }
        stressLevel = 1 - Mathf.Clamp(((monster.transform.position - this.transform.position).magnitude - 10) / 60.0f, 0, 1);
        //set heartbeat to stress
        if ((monster.transform.position - this.transform.position).magnitude < killDistance)
            this.GetComponent<KillPlayer>().kill();
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
        if (Physics.Raycast(position, rayDirection, out hit, maxDistance)&& hit.transform.gameObject != light.gameObject)
            return 0.0f;
        float distance = Vector3.Distance(position, lightPosition);
        float exposureIntensity = light.intensity / Mathf.Pow(distance, 2);
        return exposureIntensity;
    }
}
