using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject pickedObject = null;

    void Update()
    {
        if (pickedObject != null)
        {
            if (Input.GetKey("r"))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;

                AnimalBehavior animalBehavior = pickedObject.GetComponent<AnimalBehavior>();
                if (animalBehavior != null)
                {
                    animalBehavior.enabled = true;
                }

                Animator animator = pickedObject.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.enabled = true;
                }

                pickedObject.gameObject.transform.SetParent(null);
                pickedObject = null;

            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (Input.GetKey("e") && pickedObject == null)
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                Animator animator = other.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.enabled = false;
                }

                AnimalBehavior animalBehavior = other.GetComponent<AnimalBehavior>();
                if (animalBehavior != null)
                {
                    animalBehavior.enabled = false;
                }

                other.transform.position = handPoint.transform.position;
                other.gameObject.transform.SetParent(handPoint.gameObject.transform);
                pickedObject = other.gameObject;
            }
        }
    }
}
