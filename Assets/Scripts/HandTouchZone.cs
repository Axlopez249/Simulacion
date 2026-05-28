using UnityEngine;

public class HandTouchZone : MonoBehaviour
{
    public bool isLeftZone;

    public DualGrabController controller;

    private void OnTriggerEnter(Collider other)
    {
        // ZONA IZQUIERDA
        if(isLeftZone && other.CompareTag("LeftController"))
        {
            controller.leftHandDetected = true;

            Debug.Log("LEFT HAND DETECTED");
        }

        // ZONA DERECHA
        if(!isLeftZone && other.CompareTag("RightController"))
        {
            controller.rightHandDetected = true;

            Debug.Log("RIGHT HAND DETECTED");
        }

        controller.CheckBothHands();
    }

    private void OnTriggerExit(Collider other)
    {
        // IZQUIERDA
        if(isLeftZone && other.CompareTag("LeftController"))
        {
            controller.leftHandDetected = false;

            Debug.Log("LEFT HAND EXIT");
        }

        // DERECHA
        if(!isLeftZone && other.CompareTag("RightController"))
        {
            controller.rightHandDetected = false;

            Debug.Log("RIGHT HAND EXIT");
        }
    }
}