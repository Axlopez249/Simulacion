using UnityEngine;
using UnityEngine.Video;
using System.Collections;   

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

    public GameObject corazonsano;
    public GameObject corazonsanoArritmia;
    public AudioClip frenteAudio; // LOC2.1
    public AudioClip asimpleVistaAudio;
    public AudioClip observeAudio;       // LOC2.3
    public AudioClip algoCambiaAudio;    // LOC2.4
    public AudioClip tomeseSegundosAudio;// LOC2.5
    public AudioClip preguntaAudio;       // LOC2.X 
    public AudioClip latidoSanoAudio;
    public AudioClip latidoArritmiaAudio;
    public AudioClip correctoAudio;   // LOC2.6a
    public AudioClip incorrectoAudio; // LOC2.6b
    public AudioClip uncorazonsanoAudio;  // LOC2.7

    // Estos botones se colocan en la UI encima de cada corazón
    public GameObject botonIzquierdo;
    public GameObject botonDerecho;

    public AudioClip cuandoapareceAudio;
    public AudioClip muchasvecesAudio;
    public AudioClip realizaremosAudio;
    public AudioClip continuarPaso3Audio;

    public GameObject continuarPaso3Button;

    //paso3
    public GameObject hativ; // Para mostrar el dispositivo Hativ
    public AudioClip ahoraHativAudio; // LOC3.1
    public AudioClip esteDispositivoAudio; // LOC3.2
    public AudioClip piernaAudio; // LOC3.3
    public AudioClip pulgaresAudio, acontinuacionAudio, asielsistemaAudio, mientrasrealizaAudio, estoayudaraAudio, enunosAudio, continuarPaso4Audio;

    public GameObject videoPulgares; // Para mostrar el video de los pulgares
    public GameObject videoTobillo; // Para mostrar el video del tobillo
    public GameObject videoPiernaCruzada; // Para mostrar la pierna cruzada 
    public GameObject continuarPaso4Button;

    //paso4 - LOC4
    public AudioClip ahoraSuturno;
    public AudioClip crucePierna;
    public AudioClip frenteAusted;
    public AudioClip zonasIluminadas;
    public AudioClip paraTomar;
    public AudioClip seAjustara;
    public GameObject continuarPaso5Button;
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
    public void StartLOC2Step1()
    {
        // Ocultar instrucciones
        instructionsContainer.SetActive(false);

        //Activar corazones sanos y con arritmia
        corazonsano.SetActive(true);
        corazonsanoArritmia.SetActive(true);

        // Reproducir primer audio LOC2.1
        audioSource.Stop();
        audioSource.clip = frenteAudio;
        audioSource.Play();

        Debug.Log("LOC2.1 reproducido: corazones aún desactivados.");

        // Cuando termine el audio, pasar al paso 2
        StartCoroutine(WaitForAudioToEnd(audioSource.clip.length, StartLOC2Step2));
    }

    IEnumerator WaitForAudioToEnd(float duration, System.Action nextStep)
    {
        yield return new WaitForSeconds(duration);
        nextStep?.Invoke();
    }

    public void StartLOC2Step2()
    {
        // Reproducir audio LOC2.2
        audioSource.Stop();
        audioSource.clip = asimpleVistaAudio;
        audioSource.Play();

        Debug.Log("LOC2.2: Dos corazones activos con animaciones distintas.");
        StartCoroutine(PlayLOC2Step2BSequence());
    }

    IEnumerator PlayLOC2Step2BSequence()
    {
        // LOC2.5 → ambos corazones activos sin sonido externo
        corazonsano.SetActive(true);
        corazonsanoArritmia.SetActive(true);

        audioSource.clip = tomeseSegundosAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // Solo corazón sano con su audio
        corazonsano.SetActive(true);
        corazonsanoArritmia.SetActive(false);
        audioSource.clip = latidoSanoAudio;
        audioSource.Play();
        yield return new WaitForSeconds(5f);

        // Solo corazón arrítmico con su audio
        corazonsano.SetActive(false);
        corazonsanoArritmia.SetActive(true);
        audioSource.clip = latidoArritmiaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(5f);

        // Ambos corazones activos sin sonido externo
        corazonsano.SetActive(true);
        corazonsanoArritmia.SetActive(true);
        audioSource.Stop();

        // Reproducir audio de la pregunta LOC2.X
        audioSource.clip = preguntaAudio;
        audioSource.Play();

        // Aquí mostramos los botones de selección
        ShowQuestion(); 
        Debug.Log("Pregunta LOC2.X lanzada: ¿Cuál cree que presenta un ritmo irregular?");
    }

    public void ShowQuestion()
    {
        // Activar botones transparentes
        botonIzquierdo.SetActive(true);
        botonDerecho.SetActive(true);
    }

    // Método para cuando el usuario selecciona el corazón izquierdo
    public void SeleccionIzquierdo()
    {
        botonIzquierdo.SetActive(false);
        botonDerecho.SetActive(false);

        // Feedback incorrecto
        audioSource.clip = incorrectoAudio;
        audioSource.Play();
        StartCoroutine(WaitForAudioToEnd(audioSource.clip.length, NextStep));
    }

    // Método para cuando el usuario selecciona el corazón derecho
    public void SeleccionDerecho()
    {
        botonIzquierdo.SetActive(false);
        botonDerecho.SetActive(false);

        // Si el izquierdo es el arrítmico → correcto
        audioSource.clip = correctoAudio;
        audioSource.Play();

        // Aquí podés llamar al siguiente paso
        StartCoroutine(WaitForAudioToEnd2(audioSource.clip.length, NextStep));
    }

    IEnumerator WaitForAudioToEnd2(float duration, System.Action nextStep)
    {
        yield return new WaitForSeconds(duration);
        nextStep?.Invoke();
    }

    void NextStep()
    {
        // Avanzar al siguiente audio de la locución (LOC2.7)
        audioSource.clip = uncorazonsanoAudio;
        audioSource.Play();
        Debug.Log("Avanzando al siguiente paso de la locución.");
        StartCoroutine(PlayRemainingAudios());
    }
    IEnumerator PlayRemainingAudios()
    {
        // LOC2.8
        audioSource.clip = cuandoapareceAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // LOC2.9
        audioSource.clip = muchasvecesAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // LOC2.10
        audioSource.clip = realizaremosAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // LOC2.11
        corazonsano.SetActive(false);
        corazonsanoArritmia.SetActive(false);
        audioSource.clip = continuarPaso3Audio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // Mostrar botón Continuar
        continuarPaso3Button.SetActive(true);
        Debug.Log("LOC2.11 completado: mostrar botón 'Continuar'.");
    }

    // Método que se llama al presionar el botónPaso3Button
    public void OnContinuarClicked3()
    {
        Debug.Log("Botón Continuar presionado → iniciar LOC3.");
        StopAllCoroutines(); // Detener cualquier corrutina anterior
        audioSource.Stop(); // Detener audio que esté sonando
        StartLOC3();
    }

    public void StartLOC3()
    {
        StartCoroutine(PlayLOC3Sequence());
    }

    IEnumerator PlayLOC3Sequence()
    {
        // LOC3.1 → introducción a la medición
        hativ.SetActive(true); // Mostrar el dispositivo Hativ
        continuarPaso3Button.SetActive(false); // Asegurarse de ocultar el botón
        audioSource.clip = ahoraHativAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC3.1 completado: listo para mostrar la interfaz de medición.");
        Debug.Log("LOC3.1 completado.");

        // LOC3.2 → solo audio
        audioSource.clip = esteDispositivoAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC3.2 completado.");

        // LOC3.3 → activar video de doblar pierna
        audioSource.clip = piernaAudio;
        audioSource.Play();
        videoPiernaCruzada.SetActive(true);
        yield return new WaitForSeconds(audioSource.clip.length);

        // LOC5.1 → colocar pulgares
        yield return new WaitForSeconds(1f); // Pequeña pausa para transición
        audioSource.clip = pulgaresAudio;
        audioSource.Play();
        videoPiernaCruzada.SetActive(false); // Ocultar video de pierna cruzada
        videoPulgares.SetActive(true);
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC5.1 completado: videoPulgares activado.");

        // LOC3.4 → contacto en tobillo
        yield return new WaitForSeconds(1f); // Pequeña pausa para transición
        audioSource.clip = acontinuacionAudio;
        audioSource.Play();
        videoPulgares.SetActive(false); // Ocultar video de pulgares
        videoTobillo.SetActive(true);
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC3.4 completado: videoTobillo activado.");

        // LOC3.5
        yield return new WaitForSeconds(1f); // Pequeña pausa para transición
        audioSource.clip = asielsistemaAudio;
        audioSource.Play();
        videoTobillo.SetActive(false); // Ocultar video de tobillo
        yield return new WaitForSeconds(audioSource.clip.length);

        // LOC3.6
        audioSource.clip = mientrasrealizaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // LOC3.7
        audioSource.clip = estoayudaraAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // LOC3.8
        audioSource.clip = enunosAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // LOC3.9 → mostrar botón continuar
        audioSource.clip = continuarPaso4Audio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        continuarPaso4Button.SetActive(true);
        Debug.Log("LOC3.9 completado: mostrar botón 'Continuar'.");
    }

    public void OnContinuarClicked4()
    {
        Debug.Log("Botón Continuar presionado → iniciar LOC4.");
        StopAllCoroutines(); // Detener cualquier corrutina anterior
        audioSource.Stop(); // Detener audio que esté sonando
        StartLOC4();
    }

    public void StartLOC4()
    {
        StartCoroutine(PlayLOC4Sequence());
    }

    IEnumerator PlayLOC4Sequence()
    {
        continuarPaso4Button.SetActive(false);

        // LOC4.1
        audioSource.clip = ahoraSuturno;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC4.1 completado.");

        // LOC4.2 → activar video de pierna cruzada
        audioSource.clip = crucePierna;
        audioSource.Play();
        videoPiernaCruzada.SetActive(true);
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC4.2 completado.");

        // LOC4.3 → ocultar video y continuar
        yield return new WaitForSeconds(1f); // Pequeña pausa para transición
        audioSource.clip = frenteAusted;
        audioSource.Play();
        videoPiernaCruzada.SetActive(false); // Ocultar video de pierna cruzada
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC4.3 completado.");

        // LOC4.4
        audioSource.clip = zonasIluminadas;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC4.4 completado.");

        // LOC4.5
        audioSource.clip = paraTomar;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC4.5 completado.");

        // LOC4.6
        audioSource.clip = seAjustara;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Debug.Log("LOC4.6 completado: mostrar botón 'Continuar'.");
        continuarPaso5Button.SetActive(true);
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