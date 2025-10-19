using UnityEngine;

public class doorLock : MonoBehaviour
{
    public DoorOpen door;
    public Tooltip tip;

    private void Update()
    {
        if (door.canOpen)
        {
            tip.enabled = true;
        }
        else
            tip.enabled= false;
    }
}
