using UnityEngine;
using UnityEngine.UI;

public class MovimientoAnimales : MonoBehaviour
{
    public Transform jugador;
    public Slider barraHambre;
    public Slider barraComida;
    public Slider barraEnergia;
    public float velocidadEstampida = 5.0f;
    public float velocidadDeambular = 2.0f;
    public AudioClip sonidoEstampida;
    public AudioClip sonidoDescanso;

    private enum EstadoAnimal { Deambular, Estampida, Descansar }
    private EstadoAnimal estadoActual = EstadoAnimal.Deambular;
    private AudioSource audioSource;
    private Animator animalAnimator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animalAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (estadoActual)
        {
            case EstadoAnimal.Deambular:
                Deambular();
                if (barraHambre.value <= 0.3f)
                {
                    estadoActual = EstadoAnimal.Estampida;
                    animalAnimator.SetBool("EnEstampida", true);
                    audioSource.PlayOneShot(sonidoEstampida);
                }
                break;

            case EstadoAnimal.Estampida:
                Estampida();
                if (Vector3.Distance(transform.position, jugador.position) < 2.0f)
                {
                    estadoActual = EstadoAnimal.Deambular;
                    animalAnimator.SetBool("EnEstampida", false);
                    audioSource.PlayOneShot(sonidoDescanso);
                }
                else if (barraComida.value >= 1.0f)
                {
                    estadoActual = EstadoAnimal.Deambular;
                    animalAnimator.SetBool("EnEstampida", false);
                }
                else if (barraEnergia.value <= 0.1f)
                {
                    estadoActual = EstadoAnimal.Descansar;
                    animalAnimator.SetBool("EnEstampida", false);
                    audioSource.PlayOneShot(sonidoDescanso);
                }
                break;

            case EstadoAnimal.Descansar:
                Descansar();
                if (barraEnergia.value >= 1.0f)
                {
                    estadoActual = EstadoAnimal.Deambular;
                }
                break;
        }
    }

    void Deambular()
    {
        // Movimiento aleatorio simulado
        transform.Translate(Vector3.forward * velocidadDeambular * Time.deltaTime);
        barraEnergia.value = Mathf.Min(barraEnergia.value + 0.01f * Time.deltaTime, 1.0f);
    }

    void Estampida()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        transform.Translate(direccion * velocidadEstampida * Time.deltaTime);
        barraEnergia.value = Mathf.Max(barraEnergia.value - 0.05f * Time.deltaTime, 0.0f);
    }

    void Descansar()
    {
        // Recuperar energía
        barraEnergia.value = Mathf.Min(barraEnergia.value + 0.02f * Time.deltaTime, 1.0f);
    }
}
