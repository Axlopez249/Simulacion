using UnityEngine;
using UnityEngine.Video;
using System.Collections;   

public class InstructionManager : MonoBehaviour
{
    [Header("Main UI")]
    public GameObject palpitacionPanel;
    public GameObject botonPrueba;
    public GameObject cubo;
    public GameObject cubo2;
    public GameObject botonPaso2;
    
    public GameObject mesa;

    [Header("Maniquíes")]
    public GameObject normalManiqui;
    public GameObject crossedLegManiqui;

    [Header("Audio")]
    public AudioClip bienvenida;
    public AudioClip primeroMostraremos;
    public AudioClip paraSeleccionar;
    public AudioClip audioAlClickear;
    public AudioClip paraAgarrar;
    public AudioClip audioSoltar;
    public AudioClip paraMayorComodidad;
    public AudioClip todoListoAudio;
    public AudioClip cuandoesteListoAudio;
    public AudioSource audioSource;

    [Header("Simulation")]
    public VideoPlayer videoPlayer;
    public AudioSource heartbeatAudio;

    [Header("Paso 2: Arritmia")]
    [Header("Objetos")]
    public GameObject corazonsano;
    public GameObject corazonsanoArritmia;

    [Header("Botones")]
    // Estos botones se colocan en la UI encima de cada corazón
    public GameObject botonIzquierdo;
    public GameObject botonDerecho;
    public GameObject continuarPaso3Button;

    [Header("Audios")]
    public AudioClip frenteAudio; // LOC2.1

    public AudioClip primeroAparaceAudio; // LOC2.2a
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
    public AudioClip cuandoapareceAudio;
    public AudioClip muchasvecesAudio;
    public AudioClip realizaremosAudio;
    public AudioClip continuarPaso3Audio;

    //paso3
    [Header("Paso 3: Introducción dispositivo")]

    [Header("Objetos")]
    public GameObject hativ; // Para mostrar el dispositivo Hativ
    public GameObject hativSinFuncionalidad; // Para mostrar el video de los pulgares


    [Header("Botones")]
    public GameObject continuarPaso4Button;


    [Header("Audios")]
    public AudioClip ahoraHativAudio; // LOC3.1
    public AudioClip esteDispositivoAudio; // LOC3.2
    public AudioClip asielsistemaAudio, mientrasrealizaAudio, estoayudaraAudio, piernaAudio, pulgaresAudio, acontinuacionAudio, continuarPaso4Audio;

    [Header("Videos")]
    public GameObject videoPulgares; // Para mostrar el video de los pulgares
    public GameObject videoTobillo; // Para mostrar el video del tobillo
    public GameObject videoPiernaCruzada; // Para mostrar la pierna cruzada

    [Header("Nuevos Audios Paso 3-4")]
    public AudioClip piernaIzquierdaSobreDerechaAudio; 
    public AudioClip sujeteIzquierdaAudio;
    public AudioClip sujeteDerechaAudio;
    public AudioClip noSoltarDispositivoAudio;
    public AudioClip colocarEnTobilloAudio;

    [Header("Pulgares zonas")]
    public GameObject thumbLeftZone;
    public GameObject thumbRightZone;


    [Header("Paso 4: Medición")]
    [Header("Audios")]
    public AudioClip ahoraSuturno;
    public AudioClip crucePierna;
    public AudioClip frenteAusted;
    public AudioClip zonasIluminadas;
    public AudioClip paraTomar;
    public AudioClip seAjustara;

    [Header("Resultados")]

    [Header("Objetos")]

    public GameObject resultadosPanel;
    public GameObject terminarExamenButton;

    [Header("Audios")]

    // Audios LOC9.x
    public AudioClip medicionFinalizadaAudio;   // LOC9.1
    public AudioClip colocarMesaAudio;          // LOC9.2
    public AudioClip soltarBotonXAudio;         // LOC9.3
    public AudioClip resultadosExamenAudio;     // LOC9.4
    public AudioClip terminarExamenAudio;       // LOC9.5

    // Audios LOC10.x
    public AudioClip revisarResultadosAudio;    // LOC10.1
    public AudioClip edadCardiacaAudio;         // LOC10.2
    public AudioClip alteracionesAudio;         // LOC10.3
    public AudioClip actividadAltaAudio;        // LOC10.4
    public AudioClip recomendacionAudio;        // LOC10.5
    public AudioClip graciasAudio;              // LOC10.6

    void Start()
    {
        // Cuando el video termine
        //videoPlayer.loopPointReached += EndVideo;

        // Estado inicial
        //palpitacionPanel.SetActive(false);

        // Reproducir audio de bienvenida
        StartCoroutine(PlayWelcomeAudio());
    }

    // lógica del cubo
    public void OnCubeGrabbed()
    {
        if (audioAlClickear != null && audioSource != null)
        {
            audioSource.Stop(); // 🔹 detener cualquier audio previo
            audioSource.clip = audioAlClickear;
            audioSource.Play();

            // Usamos una corrutina que espera la duración real del clip
            StartCoroutine(PlayAfterAudio(audioAlClickear, PlayDropInstruction));
        }
    }

    IEnumerator PlayAfterAudio(AudioClip clip, System.Action nextStep)
    {
        if (clip != null)
        {
            yield return new WaitForSeconds(clip.length - 0.05f);
            nextStep?.Invoke();
        }
    }

    void PlayDropInstruction()
    {
        if (audioSoltar != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = audioSoltar;
            audioSource.Play();
        }
    }

    public void OnCubeReleased()
    {
        // Usuario suelta el cubo → reproducir "Muy bien" otra vez
        if (audioAlClickear != null && audioSource != null)
        {
            audioSource.clip = audioAlClickear;
            audioSource.Play();
            StartCoroutine(WaitForAudioToEnd(audioSource.clip.length, () => StartCoroutine(ContinueAfterCubeSequence())));
        }
    }

    IEnumerator ContinueAfterCubeSequence()
    {
        cubo.SetActive(false); // Ocultar el cubo después de soltarlo
        cubo2.SetActive(true); // Mostrar el segundo cubo después de soltar el primero
        //Para mayor comodidad audio
        if (paraMayorComodidad != null && audioSource != null)
        {
            audioSource.clip = paraMayorComodidad;
            audioSource.Play();
            // Puedes encadenar más audios o activar objetos aquí
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        //Todo Listo audio
        if (todoListoAudio != null && audioSource != null)
        {
            audioSource.clip = todoListoAudio;
            audioSource.Play();
            // Puedes encadenar más audios o activar objetos aquí
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        //Cuando esté listo audio
        if (cuandoesteListoAudio != null && audioSource != null)
        {
            audioSource.clip = cuandoesteListoAudio;
            audioSource.Play();
            // Puedes encadenar más audios o activar objetos aquí
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        botonPaso2.SetActive(true); // Mostrar el botón para avanzar al paso 2

    }


    IEnumerator PlayWelcomeAudio()
    {
        // Validar que audioSource exista
        if (audioSource == null)
        {
            Debug.LogError("AudioSource no está asignado");
            yield break;
        }

        // Reproducir primer audio (bienvenida)
        if (bienvenida != null)
        {
            audioSource.clip = bienvenida;
            audioSource.Play();
            yield return new WaitForSeconds(bienvenida.length - 0.05f);
        }

        // Reproducir segundo audio (primeroMostraremos)
        if (primeroMostraremos != null)
        {
            audioSource.clip = primeroMostraremos;
            audioSource.Play();
            yield return new WaitForSeconds(primeroMostraremos.length - 0.05f);
        }
        else
        {
            Debug.LogWarning("primeroMostraremos no está asignado");
        }

        // Reproducir tercer audio
        if (paraSeleccionar != null)
        {
            audioSource.clip = paraSeleccionar;
            audioSource.Play();
            
            // Activar el botón de prueba cuando comienza el audio
            if (botonPrueba != null)
            {
                botonPrueba.SetActive(true);
            }
            
            yield return new WaitForSeconds(paraSeleccionar.length - 0.05f);
        }
        else
        {
            Debug.LogWarning("paraSeleccionar no está asignado");
        }
    }

    public void OnBotonPruebaClicked()
    {
        // Desactivar el botón
        if (botonPrueba != null)
        {
            botonPrueba.SetActive(false);
        }

        // Reproducir audio al hacer click
        if (audioAlClickear != null && audioSource != null)
        {
            audioSource.clip = audioAlClickear;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("audioAlClickear no está asignado");
        }

        // Activar el cubo
        if (cubo != null)
        {
            cubo.SetActive(true);
        }

        // continuar con los audios de la locución después de un pequeño delay para que el usuario escuche el audio al clickear
        StartCoroutine(ContinueAfterClickAudio());
        
    }

    IEnumerator ContinueAfterClickAudio()
    {
        // Esperar a que termine el audio al clickear
        yield return new WaitForSeconds(audioAlClickear.length - 0.05f);

        // continuacion de audio paso 1
        if (paraAgarrar != null && audioSource != null)
        {
            audioSource.clip = paraAgarrar;
            audioSource.Play();
        }
    }

        // Método público que se llama desde el botón
    public void StartLOC2Step1()
    {   
        mesa.SetActive(false); // Ocultar la mesa para el paso 2
        cubo2.SetActive(false); // Asegurarse de ocultar el segundo cubo para el paso 2
        // Aquí no usamos yield, solo lanzamos la corrutina
        StartCoroutine(StartLOC2Step1Coroutine());
    }

    // Corrutina privada con la lógica paso a paso
    private IEnumerator StartLOC2Step1Coroutine()
    {
        botonPaso2.SetActive(false); // Ocultar el botón de paso 2

        yield return new WaitForSeconds(0.5f); // Pequeña pausa para transición
        
        //Activar corazones sanos y con arritmia
        corazonsano.SetActive(true);
        corazonsanoArritmia.SetActive(true);

        // Reproducir primer audio LOC2.1
        audioSource.Stop();
        audioSource.clip = frenteAudio;
        audioSource.Play();

        Debug.Log("LOC2.1 reproducido: corazones aún desactivados.");

        audioSource.Stop();
        audioSource.clip = primeroAparaceAudio;
        audioSource.Play();

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
        mesa.SetActive(true); // Asegurarse de ocultar la mesa para el paso 3
        StartCoroutine(PlayLOC3Sequence());
    }

    IEnumerator PlayLOC3Sequence()
    {
        hativSinFuncionalidad.SetActive(true);
        continuarPaso3Button.SetActive(false);

        // Audios iniciales
        audioSource.clip = ahoraHativAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = esteDispositivoAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = asielsistemaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = mientrasrealizaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = estoayudaraAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // Pierna cruzada
        audioSource.clip = piernaIzquierdaSobreDerechaAudio;
        audioSource.Play();
        videoPiernaCruzada.SetActive(true);

        normalManiqui.SetActive(false);
        crossedLegManiqui.SetActive(true);

        yield return new WaitForSeconds(audioSource.clip.length);
        videoPiernaCruzada.SetActive(false);

        hativSinFuncionalidad.SetActive(false);
        hativ.SetActive(true);

        // Audio y zona izquierda
        audioSource.clip = sujeteIzquierdaAudio;
        audioSource.Play();
        videoPulgares.SetActive(true);
        Debug.Log("➡️ Activando zona izquierda");
        thumbLeftZone.SetActive(true);

        Debug.Log("Esperando agarre izquierdo...");
        yield return new WaitUntil(() => FindObjectOfType<DualGrabController>().leftGrabbed);
        Debug.Log("✅ Se detectó agarre izquierdo, apagando zona");
        thumbLeftZone.SetActive(false);


        // Audio y zona derecha
        audioSource.clip = sujeteDerechaAudio;
        audioSource.Play();
        thumbRightZone.SetActive(true);

        yield return new WaitUntil(() => FindObjectOfType<DualGrabController>().rightGrabbed);

        thumbRightZone.SetActive(false);
        videoPulgares.SetActive(false);

        // Audio de no soltar
        audioSource.clip = noSoltarDispositivoAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        // Audio y video del tobillo
        audioSource.clip = colocarEnTobilloAudio;
        audioSource.Play();
        videoTobillo.SetActive(true);
        yield return new WaitForSeconds(audioSource.clip.length);
        // 👉 Aquí ya entra en acción el AnkleZoneController
        videoTobillo.SetActive(false);
    }



    public void OnSimulationCompleted()
    {
        Debug.Log("Simulación completada → iniciar secuencia final");

        StartCoroutine(PlayFinalSequence());
    }

    IEnumerator PlayFinalSequence()
    {
        audioSource.clip = medicionFinalizadaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = colocarMesaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = soltarBotonXAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = resultadosExamenAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = terminarExamenAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        terminarExamenButton.SetActive(true);
    }

    public void OnTerminarExamenClicked()
    {
        terminarExamenButton.SetActive(false);
        hativ.SetActive(false);
        mesa.SetActive(false);
        resultadosPanel.SetActive(true);

        StartCoroutine(PlayResultsSequence());
    }

    IEnumerator PlayResultsSequence()
    {
        audioSource.clip = revisarResultadosAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = edadCardiacaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = alteracionesAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = actividadAltaAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = recomendacionAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = graciasAudio;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        resultadosPanel.SetActive(false);
        Debug.Log("EXPERIENCIA COMPLETADA");
    }

    void StartSimulation()
    {
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