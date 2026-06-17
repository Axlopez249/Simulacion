using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DualGrabController : MonoBehaviour
{
    [Header("XR")]
    public XRGrabInteractable grabInteractable;

    [Header("Scene Objects")]
    public GameObject normalMannequin;
    public GameObject crossedLegMannequin;
    public GameObject ankleZone;

    [Header("Pulgares zonas")]
    public GameObject thumbLeftZone;
    public GameObject thumbRightZone;

    public bool leftGrabbed = false;
    public bool rightGrabbed = false;

    void Start()
    {
        normalMannequin.SetActive(true);
        crossedLegMannequin.SetActive(false);
        ankleZone.SetActive(false);

        thumbLeftZone.SetActive(false);
        thumbRightZone.SetActive(false);

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        string tag = args.interactorObject.transform.tag;

        if (tag == "LeftController")
        {
            leftGrabbed = true;
            thumbLeftZone.SetActive(false); // apagar zona izquierda al agarrar
            Debug.Log("Pulgar izquierdo agarrado");
        }

        if (tag == "RightController")
        {
            rightGrabbed = true;
            thumbRightZone.SetActive(false); // apagar zona derecha al agarrar
            Debug.Log("Pulgar derecho agarrado");

            if (leftGrabbed && rightGrabbed)
            {
                normalMannequin.SetActive(false);
                crossedLegMannequin.SetActive(true);
                ankleZone.SetActive(true);
                Debug.Log("Ambos pulgares agarrados → tobillo habilitado");
            }
        }
    }

    void OnReleased(SelectExitEventArgs args)
    {
        string tag = args.interactorObject.transform.tag;

        if (tag == "LeftController") leftGrabbed = false;
        if (tag == "RightController") rightGrabbed = false;

        Debug.Log("Se soltó un pulgar");
    }

    public void ActivateLeftThumbZone()
    {
        thumbLeftZone.SetActive(true);
        Debug.Log("Zona izquierda activada");
    }

    public void ActivateRightThumbZone()
    {
        thumbRightZone.SetActive(true);
        Debug.Log("Zona derecha activada");
    }
}
