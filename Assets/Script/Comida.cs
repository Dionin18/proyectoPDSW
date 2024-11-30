using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    public Rigidbody animalRigidbody;
    public Animator animator;

    public GameObject BarraComida; // El objeto que deseas activar
    public float intervalo = 5f; // Intervalo de tiempo en segundos
    public int valor = 0;

    [SerializeField] private GameObject objectToModify;

    // Referencia al script AnimalBehavior
    private AnimalBehavior animalBehavior;

    private void Start()
    {
        // Busca el script AnimalBehavior en el GameObject
        if (objectToModify != null)
        {
            animalBehavior = objectToModify.GetComponent<AnimalBehavior>();
        }

        if (valor == 0)
        {
            StartCoroutine(ActivarObjetoPeriodicamente());
            valor = 1;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Food"))
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.SetActive(false);

            animalRigidbody = objectToModify.GetComponent<Rigidbody>();
            animalRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            if (animalBehavior != null)
            {
                animalBehavior.StopAllAnimations(); // Detener todas las animaciones en AnimalBehavior.
            }
            valor = 1;
            BarraComida.gameObject.SetActive(false);
            PlayAnimation("Eat"); // Iniciar la animación de comer.

            DoDelayAction(10);
        }
    }

    void DoDelayAction(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        animalRigidbody.constraints = RigidbodyConstraints.None;

        if (animalBehavior != null)
        {
            animalBehavior.ResumeIdleAnimation(); // Regresar a la animación de Idle en AnimalBehavior.
        }

        PlayAnimation("Idle"); // Volver a Idle en el contexto de Comida.
    }

    private void PlayAnimation(string animationName)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator no está asignado.");
            return;
        }

        animator.Play(animationName);
    }
    private IEnumerator ActivarObjetoPeriodicamente()
    {
        while (valor == 0) 
        {
            yield return new WaitForSeconds(intervalo); // Espera el tiempo definido
            BarraComida.SetActive(true); // Activa el objeto
        }
    }
}