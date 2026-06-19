using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class AnkleZoneController : MonoBehaviour
{
    [Header("References")]

    [Header("Video ECG")]
    public GameObject videoECG; 

    public Transform snapPoint;

    public AudioSource audioSource;
    public AudioSource heartbeatSource;   
    [Header("Audios")]
    public AudioClip heartbeatAudio;
    public AudioClip posicionIdealAudio;

    public AudioClip mantenerEstableAudio;

    public AudioClip lecturaEnProgresoAudio;

    public AudioClip mantenerPosicionAudio; // LOC6.5a
    public AudioClip colocarDispositivoAudio; // LOC7.2
    public AudioClip mantenerEstableExtraAudio; // LOC7.3
    public AudioClip mostrarECGAudio; // LOC7.4
    public AudioClip medicionComenzadaAudio; // LOC7.5
    public AudioClip mantenerUnosSegundosAudio; // LOC8.5
    public AudioClip mostrarAvanceAudio; // LOC8.7


    [Header("Magnet Effect")]
    public float snapSpeed = 5f;

    private bool deviceInside = false;

    private Transform currentDevice;

    [Header("Maniquíes y zonas")]
    public GameObject normalMannequin;
    public GameObject crossedLegMannequin;
    public GameObject ankleZone;

    void Start()
    {
        // El panel inicia apagado
        videoECG.SetActive(false);
        videoECG.GetComponent<VideoPlayer>().loopPointReached += OnVideoFinished;
    }

    public InstructionManager instructionManager; // referencia al manager


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
            videoECG.SetActive(false);

            // Detener video
            videoECG.GetComponent<VideoPlayer>().Stop();

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

            videoECG.GetComponent<VideoPlayer>().Stop();

            videoECG.SetActive(false);
        }
    }

    /*void Update()
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
    }*/

    IEnumerator SimulationFlow()
    {
        // Esperar audio 1
        yield return new WaitForSeconds(posicionIdealAudio.length);

         // Pausa natural
        yield return new WaitForSeconds(2f);

        if(!deviceInside) yield break;

        // Audio 2: mantener estable
        audioSource.clip = mantenerEstableAudio;
        audioSource.Play();
        yield return new WaitForSeconds(mantenerEstableAudio.length);

        if(!deviceInside) yield break;

        // Mostrar video ECG
        videoECG.SetActive(true);

        heartbeatSource.clip = heartbeatAudio;
        heartbeatSource.loop = true;
        heartbeatSource.Play();

        // El VideoPlayer ya está en el objeto videoECG
        videoECG.GetComponent<VideoPlayer>().Play();

        Debug.Log("SIMULACIÓN INICIADA");


        // Audio 3: mostrar ECG
        audioSource.clip = mostrarECGAudio;
        audioSource.Play();
        yield return new WaitForSeconds(mostrarECGAudio.length);

        if(!deviceInside) yield break;

        // Audio 4: medición comenzó
        audioSource.clip = medicionComenzadaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(medicionComenzadaAudio.length);

        if(!deviceInside) yield break;

        // Audio 5: mantener unos segundos más
        audioSource.clip = mantenerUnosSegundosAudio;
        audioSource.Play();
        yield return new WaitForSeconds(mantenerUnosSegundosAudio.length);

        if(!deviceInside) yield break;

        // Audio 6: lectura en progreso (primera vez)
        yield return new WaitForSeconds(2f);
        audioSource.clip = lecturaEnProgresoAudio;
        audioSource.Play();
        yield return new WaitForSeconds(5f);

        if(!deviceInside) yield break;

        // Audio 7: lectura en progreso (segunda vez, refuerzo)
        audioSource.clip = lecturaEnProgresoAudio;
        audioSource.Play();
        yield return new WaitForSeconds(5f);

        if(!deviceInside) yield break;

        // Audio 8: mostrar avance (LOC8.7)
        audioSource.clip = mostrarAvanceAudio;
        audioSource.Play();
        yield return new WaitForSeconds(mostrarAvanceAudio.length);

        Debug.Log("SIMULACIÓN COMPLETA");
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("VIDEO TERMINADO → cerrar simulación");

        videoECG.SetActive(false);
        ankleZone.SetActive(false);

        heartbeatSource.Stop();
        heartbeatSource.loop = false;

        crossedLegMannequin.SetActive(false);
        normalMannequin.SetActive(true);

        if(instructionManager != null)
        {
            instructionManager.OnSimulationCompleted();
        }
    }

}