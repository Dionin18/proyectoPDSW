using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    public AudioSource audioSource; // Arrastra aquí tu AudioSource desde el inspector
    public float delay = 5f;        // Tiempo en segundos entre cada repetición

    [SerializeField] private AudioClip colectar1;
    [SerializeField] private AudioClip colectar2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Iniciar la corrutina para reproducir el sonido repetidamente sin usar loop
        StartCoroutine(soundOnLoop());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproduce el sonido de colección
            audioSource.PlayOneShot(colectar2);
        }
    }

    private IEnumerator soundOnLoop()
    {
        while (true) // Bucle infinito para reproducir el sonido con un delay
        {
            if (audioSource != null && colectar1 != null)
            {
                audioSource.PlayOneShot(colectar1); // Reproduce el sonido
            }
            yield return new WaitForSeconds(delay); // Espera el tiempo especificado antes de repetir
        }
    }
}
