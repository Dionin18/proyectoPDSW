using UnityEngine;
using TMPro;
using System.Collections;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationText; // Referencia al texto de notificación en el Canvas
    [SerializeField] private GameObject notificationPanel; // Panel que contiene el texto de notificación

    private void Start()
    {
        // Asegurarse de que el panel de notificación esté desactivado al inicio
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false); // Desactivar el panel de notificación para que no sea visible al comenzar el juego
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colisiona tiene la etiqueta "Item"
        if (other.CompareTag("Item"))
        {
            string itemName = other.gameObject.name; // Obtiene el nombre del objeto con el que se colisionó
            ShowNotification(itemName); // Muestra una notificación con el nombre del objeto

            // Desactiva temporalmente el objeto recogido (en lugar de destruirlo)
            other.gameObject.SetActive(false);
        }
    }

    void ShowNotification(string itemName)
    {
        // Verifica si el texto de notificación y el panel de notificación no son nulos
        if (notificationText != null && notificationPanel != null)
        {
            notificationText.text = $"Felicidades, consiguió un {itemName}"; // Establece el mensaje de notificación con el nombre del objeto
            notificationPanel.SetActive(true); // Muestra el panel de notificación

            // Inicia la corrutina para ocultar la notificación después de unos segundos
            StartCoroutine(HideNotificationCoroutine());
        }
    }

    IEnumerator HideNotificationCoroutine()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos antes de ocultar la notificación
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false); // Oculta el panel de notificación
        }
    }
}
