using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DualGrabController : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;

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

            // SOLO si aún no hay otra mano
            if(!rightGrab)
            {
                grabInteractable.attachTransform = leftAttachPoint;
            }

            Debug.Log("LEFT HAND DETECTED");
        }

        // CONTROL DERECHO
        if(tag == "RightController")
        {
            rightGrab = true;

            // SOLO si aún no hay otra mano
            if(!leftGrab)
            {
                grabInteractable.attachTransform = rightAttachPoint;
            }

            Debug.Log("RIGHT HAND DETECTED");
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

        // Si NO están ambas manos
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

            // Desbloquear movimiento
            grabInteractable.trackPosition = true;

            grabInteractable.trackRotation = true;
        }
    }
}