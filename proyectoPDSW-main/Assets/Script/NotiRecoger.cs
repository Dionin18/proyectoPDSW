using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotiRecoger : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI TextoRecoger; // Referencia al texto de notificación en el Canvas
    [SerializeField] private GameObject panelRecoger; // Panel que contiene el texto de notificación

    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();

        if (collision.CompareTag("Item"))
        {
            Debug.Log(collision.gameObject.name);
            string itemName = collision.gameObject.name; // Obtiene el nombre del objeto con el que se colisionó
            ShowNotification(itemName); // Muestra una notificación con el nombre del objeto

        }

    }

    void ShowNotification(string itemName)
    {
        // Verifica si el texto de notificación y el panel de notificación no son nulos
        if (TextoRecoger != null && panelRecoger != null)
        {
            panelRecoger.SetActive(true); // Muestra el panel de notificación
            TextoRecoger.text = $"Felicidades, consiguió un {itemName}"; // Establece el mensaje de notificación con el nombre del objeto

            // Inicia la corrutina para ocultar la notificación después de unos segundos
            StartCoroutine(HideNotificationCoroutine());
        }
    }

    IEnumerator HideNotificationCoroutine()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos antes de ocultar la notificación
        if (panelRecoger != null)
        {
            panelRecoger.SetActive(false); // Oculta el panel de notificación
        }
    }

}
