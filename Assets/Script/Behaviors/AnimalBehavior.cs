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

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.Play("Locomotion");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {

            isMoving = true;
            StartCoroutine(Moving());
        }

    }

    public IEnumerator Moving()
    {
        Debug.Log("Se está moviendo");
        Vector3 currentPosition = transform.position;
        targetPosition = currentPosition + new Vector3(Random.Range(-2f,2f),
                                                transform.position.y,
                                                Random.Range(-2f,2f));
        float _timeMove = timeMove;
        while (_timeMove>0)
        {
            Vector3 direction = (targetPosition - currentPosition).normalized;
            Quaternion animalRotation = Quaternion.LookRotation(direction);
            rbody.AddForce(direction*moveForce, ForceMode.Force);
            currentPosition = transform.position;
            _timeMove -= Time.deltaTime;
            rbody.rotation = animalRotation;
        }
        animator.Play("Eat");
        yield return new WaitForSeconds(Random.Range(0.4f,4f));
        isMoving = false;
        animator.Play("Locomotion");
        yield return null;
    }
}
