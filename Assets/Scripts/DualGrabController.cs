using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DualGrabController : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    public Transform leftAttachPoint;

    public Transform rightAttachPoint;

    private bool leftGrab = false;

    private bool rightGrab = false;

    void Start()
    {
        // Bloqueado inicialmente
        grabInteractable.trackPosition = false;

        grabInteractable.trackRotation = false;

        grabInteractable.selectEntered.AddListener(OnGrab);

        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        string tag = args.interactorObject.transform.tag;

        // CONTROL IZQUIERDO
        if(tag == "LeftController")
        {
            leftGrab = true;

            grabInteractable.attachTransform = leftAttachPoint;

            Debug.Log("LEFT ATTACH");
        }

        // CONTROL DERECHO
        if(tag == "RightController")
        {
            rightGrab = true;

            grabInteractable.attachTransform = rightAttachPoint;

            Debug.Log("RIGHT ATTACH");
        }

        CheckBothHands();
    }

    void OnRelease(SelectExitEventArgs args)
    {
        string tag = args.interactorObject.transform.tag;

        if(tag == "LeftController")
        {
            leftGrab = false;
        }

        if(tag == "RightController")
        {
            rightGrab = false;
        }

        // Si no están ambas manos
        // bloquear nuevamente
        if(!(leftGrab && rightGrab))
        {
            grabInteractable.trackPosition = false;

            grabInteractable.trackRotation = false;

            Debug.Log("OBJETO BLOQUEADO");
        }
    }

    void CheckBothHands()
    {
        if(leftGrab && rightGrab)
        {
            Debug.Log("DOS CONTROLES DETECTADOS");

            // Ahora sí mover
            grabInteractable.trackPosition = true;

            grabInteractable.trackRotation = true;
        }
    }
}