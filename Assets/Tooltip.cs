using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private bool showTips;
    public TextMeshPro tip;
    private void Start()
    {
        tip.enabled = false; 
    }
    private void OnTriggerEnter(Collider other)
    {
        tip.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        tip.enabled = false;
    }
}
