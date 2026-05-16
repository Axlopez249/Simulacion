using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject instructionPanel;

    public void StartSimulation()
    {
        mainMenuPanel.SetActive(false);
        instructionPanel.SetActive(true);
    }
}