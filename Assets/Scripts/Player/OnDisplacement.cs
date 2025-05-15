using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisplacement : MonoBehaviour
{
    /// Umbral de desplazamiento (por ejemplo, 5 unidades)
    public float distanceThreshold = 5f;

    public SoundEmitter soundEmitter;
    // Lista de clips de audio para reproducir
    public AudioClipData[] soundClips;

    // AudioSource para reproducir los sonidos
    private AudioSource audioSource;

    // Almacena la posici�n inicial
    private Vector3 initialPosition;

    // Distancia total recorrida
    private float accumulatedDistance = 0f;

    void Start()
    {
        // Establece la posici�n inicial
        initialPosition = transform.position;

        // Obtiene el AudioSource del GameObject o a�ade uno si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Calcula la distancia recorrida desde la �ltima posici�n
        float distanceSinceLastFrame = Vector3.Distance(initialPosition, transform.position);

        // Acumula la distancia
        accumulatedDistance += distanceSinceLastFrame;

        // Si se ha recorrido una distancia mayor al umbral, ejecuta la acci�n
        if (accumulatedDistance >= distanceThreshold)
        {
            PlayRandomSound();
            accumulatedDistance = 0f; // Reinicia el contador
        }

        // Actualiza la posici�n inicial para el pr�ximo c�lculo
        initialPosition = transform.position;
    }

    // M�todo para reproducir un sonido aleatorio
    void PlayRandomSound()
    {
        if (soundClips.Length > 0)
        {
            // Selecciona un clip aleatorio de la lista
            AudioClip randomClip = soundClips[Random.Range(0, soundClips.Length)].clip;
            soundEmitter.EmitSound();
            audioSource.PlayOneShot(randomClip);
            //Debug.Log("�Sonido reproducido!");
        }
        else
        {
            Debug.LogWarning("No hay sonidos asignados en la lista.");
        }
    }
}
