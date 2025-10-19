using System.Collections.Generic;
using UnityEngine;

public class StaticScript : MonoBehaviour
{
    public Material material;
    public List<MeshCollider> meshColliders;
    public GameObject player;
    public GameObject monster;
    public float damageLevel;
    public float killDistance = 1.0f;
    void Start()
    {
        material.SetFloat("_Alpha", 0);
        damageLevel = 0;
    }
    //call every light calculation
    public void setStatic(float lightLevel, float threshold)
    {
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
            damageLevel -= 0.05f;
        else
        {
            bool col = false;
            foreach (MeshCollider mesh in meshColliders)
                if (mesh.bounds.Contains(player.transform.position))
                { 
                    col = true;
                    break;
                }
            if (col)
            {
                damageLevel += 0.003f;
                player.GetComponent<GrueBehavior>().hasGrue = true;
            }
            else
                damageLevel -= 0.05f;
        }
        material.SetFloat("_Alpha", damageLevel);
        if (damageLevel >= 1.0f)
        {
            player.GetComponent<KillPlayer>().kill();
        }
    }
}
