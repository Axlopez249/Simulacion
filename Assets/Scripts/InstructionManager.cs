using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject mainMenuPanel;

    public GameObject instructionsContainer;

    public GameObject[] instructionPanels;

    public AudioClip[] instructionAudios;

    public AudioSource audioSource;

    private int currentInstruction = 0;

    public void StartInstructions()
    {
        mainMenuPanel.SetActive(false);

        instructionsContainer.SetActive(true);

        currentInstruction = 0;

        instructionPanels[currentInstruction].SetActive(true);

        PlayCurrentAudio();
    }

    public void NextInstruction()
    {
        instructionPanels[currentInstruction].SetActive(false);

        currentInstruction++;

        if(currentInstruction < instructionPanels.Length)
        {
            instructionPanels[currentInstruction].SetActive(true);

            PlayCurrentAudio();
        }
        else
        {
            StartSimulation();
        }
    }

    void PlayCurrentAudio()
    {
        audioSource.Stop();

        audioSource.clip = instructionAudios[currentInstruction];

        audioSource.Play();
    }

    void StartSimulation()
    {
        instructionsContainer.SetActive(false);

        Debug.Log("SIMULACIÓN INICIADA");
    }
}