using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraComida : MonoBehaviour
{
    public GameObject objeto; // El objeto que deseas activar
    public float intervalo = 5f; // Intervalo de tiempo en segundos
    public int valor = 0;

    private void Start()
    {
        if(valor == 0)
        {
            StartCoroutine(ActivarObjetoPeriodicamente());
            valor = 1;
        }
    }

    private IEnumerator ActivarObjetoPeriodicamente()
    {
        while (valor == 0) // Bucle infinito para repetir la activación
        {
            yield return new WaitForSeconds(intervalo); // Espera el tiempo definido
            objeto.SetActive(true); // Activa el objeto
        }
    }
}