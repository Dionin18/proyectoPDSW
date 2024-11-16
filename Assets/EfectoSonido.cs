using System.Collections;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    public AudioSource audioSource; // Arrastra aqu� tu AudioSource desde el inspector
    public float delay = 2f;        // Tiempo en segundos entre cada repetici�n

    void Start()
    {
        // Inicia la corrutina para reproducir el sonido con delay
        StartCoroutine(PlaySoundWithDelay());
    }

    IEnumerator PlaySoundWithDelay()
    {
        while (true) // Bucle infinito para repetir el sonido
        {
            audioSource.Play(); // Reproduce el sonido
            yield return new WaitForSeconds(delay); // Espera el tiempo especificado antes de repetir
        }
    }
}
