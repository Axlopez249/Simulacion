using UnityEngine;

public class DualGrabController : MonoBehaviour
{
    [Header("Scene Objects")]
    public GameObject crossLegMannequin;

    public GameObject ankleZone;

    [HideInInspector]
    public bool leftHandDetected = false;

    [HideInInspector]
    public bool rightHandDetected = false;

    private bool alreadyActivated = false;

    void Start()
    {
        // Apagados al inicio
        crossLegMannequin.SetActive(false);

        ankleZone.SetActive(false);
    }

    public void CheckBothHands()
    {
        if(leftHandDetected && rightHandDetected)
        {
            Debug.Log("AMBAS MANOS DETECTADAS");

            // Solo una vez
            if(!alreadyActivated)
            {
                alreadyActivated = true;

                // Activar maniquí
                crossLegMannequin.SetActive(true);

                // Activar zona tobillo
                ankleZone.SetActive(true);

                Debug.Log("MANIQUÍ Y TOBILLO ACTIVADOS");
            }
        }
    }
}