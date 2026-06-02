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

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sensoresAudio;

    private bool alreadyActivated = false;

    void Start()
    {
        normalMannequin.SetActive(true);

        crossedLegMannequin.SetActive(false);

        ankleZone.SetActive(false);

        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        // evitar repetir
        if(alreadyActivated)
            return;

        alreadyActivated = true;

        // SOLO cambiar maniquí
        normalMannequin.SetActive(false);

        crossedLegMannequin.SetActive(true);

        ankleZone.SetActive(true);

        // Reproducir audio "sensores"
        if(audioSource != null && sensoresAudio != null)
        {
            audioSource.clip = sensoresAudio;
            audioSource.Play();
        }

        Debug.Log("ANKLE ZONE ACTIVADA");
    }
}