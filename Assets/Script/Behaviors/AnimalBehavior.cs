using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalBehavior : MonoBehaviour
{
    public Rigidbody rbody;
    public Animator animator;
    public bool isMoving;
    public Vector3 targetPosition;
    public float moveForce;
    public float timeMove;

    private bool isPaused;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        PlayIdleAnimation(); // Empieza con una animación estática.
    }

    void Update()
    {
        if (!isMoving && !isPaused)
        {
            isMoving = true;
            StartCoroutine(Moving());
        }
    }

    public IEnumerator Moving()
    {
        Vector3 currentPosition = transform.position;
        targetPosition = currentPosition + new Vector3(
            Random.Range(-2f, 2f),
            transform.position.y,
            Random.Range(-2f, 2f)
        );

        float _timeMove = timeMove;
        PlayWalkAnimation(); // Animación al comenzar a moverse.

        while (_timeMove > 0 && !isPaused)
        {
            Vector3 direction = (targetPosition - currentPosition).normalized;
            Quaternion animalRotation = Quaternion.LookRotation(direction);
            rbody.AddForce(direction * moveForce, ForceMode.Force);
            currentPosition = transform.position;
            _timeMove -= Time.deltaTime;
            rbody.rotation = animalRotation;
        }

        if (!isPaused)
        {
            PlayEatAnimation(); // Animación de comer.
        }

        yield return new WaitForSeconds(Random.Range(0.4f, 4f));
        isMoving = false;

        if (!isPaused)
        {
            PlayIdleAnimation(); // Regresa a la animación de inactividad.
        }

        yield return null;
    }

    // Métodos para detener y reanudar animaciones.
    public void StopAllAnimations()
    {
        isPaused = true;
        animator.Play("Idle"); // Asegura que se quede en Idle mientras está pausado.
    }

    public void ResumeIdleAnimation()
    {
        isPaused = false;
        PlayIdleAnimation(); // Regresa a Idle después de pausar.
    }

    private void PlayWalkAnimation()
    {
        animator.Play("Locomotion");
    }

    private void PlayEatAnimation()
    {
        animator.Play("Eat");
    }

    private void PlayIdleAnimation()
    {
        animator.Play("Idle");
    }
}