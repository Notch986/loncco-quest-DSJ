using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] GameObject visibleBottle; // Referencia al objeto visible intacto
    [SerializeField] GameObject brokenBottle;  // Referencia al objeto roto (ya en la jerarqu�a)
    [SerializeField] SoundEmitter soundEmitter; // Referencia al emisor de sonido

    void Start()
    {
        // Asegurarse de que la botella rota est� desactivada al inicio
        if (brokenBottle != null)
        {
            brokenBottle.SetActive(false);
        }
    }

    void Explode()
    {
        if (brokenBottle != null && visibleBottle != null)
        {
            soundEmitter.EmitSound();
            // Desactivar la botella intacta y activar la rota
            visibleBottle.SetActive(false);
            brokenBottle.SetActive(true);

            // Opcional: aplicar fuerzas a los fragmentos si es necesario
            brokenBottle.GetComponent<BrokenBottle>()?.RandomVelocities();
        }
        else
        {
            Debug.LogWarning("No se han asignado las referencias de la botella");
        }
    }

    // Explota al colisionar con suficiente velocidad
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name + " at " + collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > 10)
        {
            Explode();
        }
    }
}
