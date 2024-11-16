using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectable : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI TextoRecoger; // Referencia al texto de notificación en el Canvas
    [SerializeField] private GameObject panelRecoger; // Panel que contiene el texto de notificación
    [SerializeField] private GameObject objectToDeactivate;



    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            if(Inventario.instance.Bag.Count <= 12)
            {
                Inventario.instance.AgregarAlInventario(objectToDeactivate);


                objectToDeactivate.SetActive(false);

            }
        }

    }

}