using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DualGrabController : MonoBehaviour
{
    [Header("XR")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

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

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        string name = args.interactorObject.transform.name;
        string parentName = args.interactorObject.transform.parent.name;

        Debug.Log("OnGrabbed ejecutado por: " + name + " (padre: " + parentName + ")");

        if (parentName.Contains("Left"))
        {
            leftGrabbed = true;
            Debug.Log("Mano izquierda detectada");
        }

        if (parentName.Contains("Right"))
        {
            rightGrabbed = true;
            Debug.Log("Mano derecha detectada");

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
        string name = args.interactorObject.transform.name;
        Debug.Log("OnReleased ejecutado por: " + args.interactorObject.transform.name);

        if (name.Contains("Left")) leftGrabbed = false;
        if (name.Contains("Right")) rightGrabbed = false;

        Debug.Log("Se soltó una mano");
    }
}
