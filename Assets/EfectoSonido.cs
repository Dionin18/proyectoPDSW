using System.Collections;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay = 8f;

    [SerializeField] private AudioClip colectar1;
    [SerializeField] private AudioClip colectar2;

    void Start()
    {
        StartCoroutine(IniciarSonidoConDelay()); // Iniciar la corrutina para repetir el sonido cada cierto intervalo
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop(); // Detener el sonido en loop antes de reproducir el de recolección
            audioSource.PlayOneShot(colectar2);
        }
    }

    private IEnumerator IniciarSonidoConDelay()
    {
        while (true)
        {
            if (audioSource != null && colectar1 != null)
            {
                audioSource.clip = colectar1;
                audioSource.Play(); // Reproduce el sonido
            }
            yield return new WaitForSeconds(delay); // Espera el tiempo especificado antes de repetir
        }
    }
}
