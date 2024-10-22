using UnityEngine;

public class MovementP : MonoBehaviour
{
    public float velocidad = 5.0f;

    void Update()
    {
        Vector3 movimiento = Vector3.zero;

        // Mover hacia adelante y atrás
        if (Input.GetKey(KeyCode.W))
        {
            movimiento += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movimiento += Vector3.back;
        }

        // Mover hacia la izquierda y la derecha
        if (Input.GetKey(KeyCode.A))
        {
            movimiento += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movimiento += Vector3.right;
        }

        // Aplicar el movimiento al personaje
        transform.Translate(movimiento * velocidad * Time.deltaTime);
    }
}
