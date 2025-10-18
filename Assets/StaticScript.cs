using System.Collections.Generic;
using UnityEngine;

public class StaticScript : MonoBehaviour
{
    public Material material;
    public List<MeshCollider> meshColliders;
    public GameObject player;
    private float damageLevel;
    void Start()
    {
        material.SetFloat("_Alpha", 0);
        damageLevel = 0;
    }
    //call every light calculation
    public void setStatic(float lightLevel, float threshhold)
    {
        if (lightLevel > threshhold)
            damageLevel -= 0.01f;
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
                damageLevel += 0.01f;
            else
                damageLevel -= 0.01f;
        }
        material.SetFloat("_Alpha", damageLevel);
        if (damageLevel >= 1.0f)
        {
            player.GetComponent<KillPlayer>().kill();
        }
    }
}
