using UnityEngine;

public class ManejoAudios : MonoBehaviour
{
    public AudioSource audioSource;      // El AudioSource de la cámara
    public AudioClip Bienvenida;         // Clip de bienvenida

    void Start()
    {
        // Reproduce bienvenida apenas inicia
        audioSource.clip = Bienvenida;
        audioSource.Play();
    }
}
