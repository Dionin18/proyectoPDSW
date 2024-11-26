using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    public Rigidbody animalRigidbody;
    public Animator animator;

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

            PlayAnimation("Eat"); // Iniciar la animaci�n de comer.

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
            animalBehavior.ResumeIdleAnimation(); // Regresar a la animaci�n de Idle en AnimalBehavior.
        }

        PlayAnimation("Idle"); // Volver a Idle en el contexto de Comida.
    }

    private void PlayAnimation(string animationName)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator no est� asignado.");
            return;
        }

        animator.Play(animationName);
    }
}