using UnityEngine;


public class FingerPlacementManager : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private bool leftTouching = false;
    private bool rightTouching = false;

    public void SetLeftTouch(bool value)
    {
        leftTouching = value;
        CheckBothTouches();
    }

    public void SetRightTouch(bool value)
    {
        rightTouching = value;
        CheckBothTouches();
    }

    void CheckBothTouches()
    {
        if(leftTouching && rightTouching)
        {
            grabInteractable.enabled = true;

            Debug.Log("Dispositivo desbloqueado");
        }
        else
        {
            grabInteractable.enabled = false;
        }
    }
}