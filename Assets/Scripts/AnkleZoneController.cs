using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class AnkleZoneController : MonoBehaviour
{
    [Header("References")]
    public Transform snapPoint;

    public GameObject ecgPanel;

    public VideoPlayer videoPlayer;

    public AudioSource audioSource;

    [Header("Audios")]
    public AudioClip posicionIdealAudio;

    public AudioClip mantenerEstableAudio;

    public AudioClip lecturaEnProgresoAudio;

    [Header("Magnet Effect")]
    public float snapSpeed = 5f;

    private bool deviceInside = false;

    private Transform currentDevice;

    void Start()
    {
        // El panel inicia apagado
        ecgPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MedicalDevice"))
        {
            Debug.Log("DISPOSITIVO DETECTADO");

            currentDevice = other.transform;

            deviceInside = true;

            // Reiniciar cualquier flujo anterior
            StopAllCoroutines();

            // Ocultar ECG por si estaba activo
            ecgPanel.SetActive(false);

            // Detener video
            videoPlayer.Stop();

            // Audio inicial
            audioSource.Stop();

            audioSource.clip = posicionIdealAudio;

            audioSource.Play();

            // Iniciar flujo completo
            StartCoroutine(SimulationFlow());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("MedicalDevice"))
        {
            Debug.Log("DISPOSITIVO REMOVIDO");

            deviceInside = false;

            // Detener todo
            StopAllCoroutines();

            audioSource.Stop();

            videoPlayer.Stop();

            ecgPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Efecto imán suave
        if(deviceInside && currentDevice != null)
        {
            currentDevice.position = Vector3.Lerp(
                currentDevice.position,
                snapPoint.position,
                Time.deltaTime * snapSpeed
            );

            currentDevice.rotation = Quaternion.Lerp(
                currentDevice.rotation,
                snapPoint.rotation,
                Time.deltaTime * snapSpeed
            );
        }
    }

    IEnumerator SimulationFlow()
    {
        // Esperar audio 1
        yield return new WaitForSeconds(posicionIdealAudio.length);

        // Pausa natural
        yield return new WaitForSeconds(3f);

        // Verificar si sigue en el tobillo
        if(!deviceInside)
            yield break;

        // Audio 2
        audioSource.clip = mantenerEstableAudio;

        audioSource.Play();

        // Esperar audio 2
        yield return new WaitForSeconds(mantenerEstableAudio.length);

        // Verificar nuevamente
        if(!deviceInside)
            yield break;

        // Mostrar panel ECG
        ecgPanel.SetActive(true);

        // Reproducir video
        videoPlayer.Play();

        Debug.Log("SIMULACIÓN INICIADA");

        // Esperar 2 segundos
        yield return new WaitForSeconds(2f);

        // Verificar nuevamente
        if(!deviceInside)
            yield break;

        // Audio lectura en progreso
        audioSource.clip = lecturaEnProgresoAudio;

        audioSource.Play();
    }
}