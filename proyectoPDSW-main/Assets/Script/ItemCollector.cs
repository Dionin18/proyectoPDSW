using UnityEngine;
using TMPro;
using System.Collections;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationText; // Referencia al texto de notificaci�n en el Canvas
    [SerializeField] private GameObject notificationPanel; // Panel que contiene el texto de notificaci�n

    private void Start()
    {
        // Asegurarse de que el panel de notificaci�n est� desactivado al inicio
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false); // Desactivar el panel de notificaci�n para que no sea visible al comenzar el juego
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colisiona tiene la etiqueta "Item"
        if (other.CompareTag("Item"))
        {
            string itemName = other.gameObject.name; // Obtiene el nombre del objeto con el que se colision�
            ShowNotification(itemName); // Muestra una notificaci�n con el nombre del objeto

            // Desactiva temporalmente el objeto recogido (en lugar de destruirlo)
            other.gameObject.SetActive(false);
        }
    }

    void ShowNotification(string itemName)
    {
        // Verifica si el texto de notificaci�n y el panel de notificaci�n no son nulos
        if (notificationText != null && notificationPanel != null)
        {
            notificationText.text = $"Felicidades, consigui� un {itemName}"; // Establece el mensaje de notificaci�n con el nombre del objeto
            notificationPanel.SetActive(true); // Muestra el panel de notificaci�n

            // Inicia la corrutina para ocultar la notificaci�n despu�s de unos segundos
            StartCoroutine(HideNotificationCoroutine());
        }
    }

    IEnumerator HideNotificationCoroutine()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos antes de ocultar la notificaci�n
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false); // Oculta el panel de notificaci�n
        }
    }
}
