using System.Collections.Generic;
using UnityEngine;

public class StaticScript : MonoBehaviour
{
    public Material material;
    public List<GameObject> grueAreas;
    private List<MeshFilter> meshFilters = new List<MeshFilter>();
    public GameObject player;
    public GameObject monster;
    public float damageLevel;
    public float killDistance = 1.7f;
    public float stressLevel;
    void Start()
    {
        material.SetFloat("_Alpha", 0);
        damageLevel = 0;
        stressLevel = 0;
        foreach (GameObject area in grueAreas)
            meshFilters.Add(area.GetComponent<MeshFilter>().GetComponent<MeshFilter>());
    }
    //call every light calculation
    public void setStatic(float lightLevel, float threshold)
    {
        stressLevel = Mathf.Max(1 - Mathf.Clamp(((monster.transform.position - player.transform.position).magnitude - 10) / 60.0f, 0, 1), damageLevel);
       // print("Light: " + lightLevel + " tress: "+stressLevel + " Damage: "+damageLevel + " Dist: "+(1 - Mathf.Clamp(((monster.transform.position - player.transform.position).magnitude - 10) / 60.0f, 0, 1)));
        if ((monster.transform.position - player.transform.position).magnitude < killDistance)
            player.GetComponent<KillPlayer>().kill(); 
        if (player.GetComponent<GrueBehavior>().hasGrue && damageLevel <= 0.1f)
            player.GetComponent<GrueBehavior>().hasGrue = false;
        if (player.GetComponent<GrueBehavior>().hasGrue && lightLevel >= 1.0f)
        {
            player.GetComponent<GrueBehavior>().hasGrue = false;
            player.GetComponent<GrueBehavior>().DoGrue();
        }
        if (lightLevel > threshold)
            damageLevel = Mathf.Max(0, damageLevel - 0.05f);
        else
        {
            bool col = false;
            foreach (MeshFilter mesh in meshFilters)
                if (Physics.CheckSphere(player.transform.position, 0.5f, 1 << mesh.gameObject.layer))
                {
                    col = true;
                    break;
                }
            if (col)
            {
                damageLevel = Mathf.Min(1.0f, damageLevel + 0.001f);
                player.GetComponent<GrueBehavior>().hasGrue = true;
            }
            else
                damageLevel = Mathf.Max(0, damageLevel - 0.03f);
        }
        material.SetFloat("_Alpha", damageLevel/2.0f);
        if (damageLevel >= 1.0f)
        {
            player.GetComponent<KillPlayer>().kill();
        }
    }
}
