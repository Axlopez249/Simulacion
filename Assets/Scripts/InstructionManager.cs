using UnityEngine;
using UnityEngine.Video;

public class InstructionManager : MonoBehaviour
{
    [Header("Main UI")]
    public GameObject mainMenuPanel;

    public GameObject instructionsContainer;

    public GameObject palpitacionPanel;

    [Header("Instructions")]
    public GameObject[] instructionPanels;

    public AudioClip[] instructionAudios;

    public AudioSource audioSource;

    [Header("Simulation")]
    public VideoPlayer videoPlayer;

    public AudioSource heartbeatAudio;

    private int currentInstruction = 0;

    void Start()
    {
        // Cuando el video termine
        videoPlayer.loopPointReached += EndVideo;

        // Estado inicial
        mainMenuPanel.SetActive(true);

        instructionsContainer.SetActive(false);

        palpitacionPanel.SetActive(false);
    }

    public void StartInstructions()
    {
        // Ocultar menú principal
        mainMenuPanel.SetActive(false);

        // Mostrar contenedor de instrucciones
        instructionsContainer.SetActive(true);

        // Reiniciar índice
        currentInstruction = 0;

        // Apagar todos los paneles primero
        for (int i = 0; i < instructionPanels.Length; i++)
        {
            instructionPanels[i].SetActive(false);
        }

        // Mostrar primera instrucción
        instructionPanels[currentInstruction].SetActive(true);

        // Reproducir audio correspondiente
        PlayCurrentAudio();
    }

    public void NextInstruction()
    {
        // Apagar instrucción actual
        instructionPanels[currentInstruction].SetActive(false);

        // Siguiente instrucción
        currentInstruction++;

        // Si aún hay instrucciones
        if (currentInstruction < instructionPanels.Length)
        {
            instructionPanels[currentInstruction].SetActive(true);

            PlayCurrentAudio();
        }
        else
        {
            // Iniciar simulación
            StartSimulation();
        }
    }

    void PlayCurrentAudio()
    {
        // Validación
        if (currentInstruction >= instructionAudios.Length)
            return;

        audioSource.Stop();

        audioSource.clip = instructionAudios[currentInstruction];

        audioSource.Play();
    }

    void StartSimulation()
    {
        // Ocultar instrucciones
        instructionsContainer.SetActive(false);

        // Mostrar panel de simulación
        palpitacionPanel.SetActive(true);

        // Iniciar video
        videoPlayer.Play();

        // Iniciar audio de latidos
        heartbeatAudio.Play();

        Debug.Log("SIMULACIÓN INICIADA");
    }

    void EndVideo(VideoPlayer vp)
    {
        // Detener audio cuando termine el video
        heartbeatAudio.Stop();

        Debug.Log("VIDEO TERMINADO");
    }
}