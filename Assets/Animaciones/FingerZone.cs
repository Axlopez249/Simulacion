using UnityEngine;

public class FingerZone : MonoBehaviour
{
    public bool isLeftZone;

    public FingerPlacementManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if(isLeftZone && other.CompareTag("LeftController"))
        {
            manager.SetLeftTouch(true);
        }

        if(!isLeftZone && other.CompareTag("RightController"))
        {
            manager.SetRightTouch(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(isLeftZone && other.CompareTag("LeftController"))
        {
            manager.SetLeftTouch(false);
        }

        if(!isLeftZone && other.CompareTag("RightController"))
        {
            manager.SetRightTouch(false);
        }
    }
}