using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionDiaNoche : MonoBehaviour
{
    public float speed = 10f; // Ajusta la velocidad de rotación aquí

    void Update()
    {
        // Rota el objeto en el eje Y
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
