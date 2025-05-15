using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openAngle = -90f; // �ngulo final en el eje Y para abrir la puerta
    [SerializeField] private float openSpeed = 2f;   // Velocidad de apertura
    private bool isOpen = false; // Para evitar m�ltiples aperturas

    public string doorTag = "Door1"; // Tag requerido para abrir esta puerta

    // Intentar abrir la puerta si coincide el tag y el jugador tiene la llave
    public void TryOpenDoor(bool hasKey, string requiredTag)
    {
        if (isOpen)
            return; // Si ya est� abierta, no hacer nada

        if (hasKey && doorTag == requiredTag)
        {
            Debug.Log("La puerta correcta se abre");
            StartCoroutine(OpenDoor());
        }
        else if (hasKey)
        {
            Debug.Log("No es la puerta correcta");
        }
        else
        {
            Debug.Log("Necesitas la llave para abrir esta puerta");
        }
    }

    private IEnumerator OpenDoor()
    {
        isOpen = true; // Marcar la puerta como abierta

        Quaternion startRotation = transform.rotation; // Rotaci�n inicial
        Quaternion targetRotation = Quaternion.Euler(0f, openAngle, 0f); // Rotaci�n final

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        transform.rotation = targetRotation; // Asegurar que la rotaci�n final sea precisa
    }
}
