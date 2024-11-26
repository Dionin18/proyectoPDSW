using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotiRecoger : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI TextoRecoger; // Referencia al texto de notificaci�n en el Canvas
    [SerializeField] private GameObject panelRecoger; // Panel que contiene el texto de notificaci�n

    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();

        if (collision.CompareTag("Item"))
        {
            Debug.Log(collision.gameObject.name);
            string itemName = collision.gameObject.name; // Obtiene el nombre del objeto con el que se colision�
            ShowNotification(itemName); // Muestra una notificaci�n con el nombre del objeto

        }

    }

    void ShowNotification(string itemName)
    {
        // Verifica si el texto de notificaci�n y el panel de notificaci�n no son nulos
        if (TextoRecoger != null && panelRecoger != null)
        {
            panelRecoger.SetActive(true); // Muestra el panel de notificaci�n
            TextoRecoger.text = $"Felicidades, consigui� un {itemName}"; // Establece el mensaje de notificaci�n con el nombre del objeto

            // Inicia la corrutina para ocultar la notificaci�n despu�s de unos segundos
            StartCoroutine(HideNotificationCoroutine());
        }
    }

    IEnumerator HideNotificationCoroutine()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos antes de ocultar la notificaci�n
        if (panelRecoger != null)
        {
            panelRecoger.SetActive(false); // Oculta el panel de notificaci�n
        }
    }

}
