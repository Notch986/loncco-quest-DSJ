using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab; // Prefab de la llave.
    [SerializeField] private Transform[] keySpawnPoints; // Puntos de aparici�n de la llave.

    void Start()
    {
        SpawnKeyAtRandomLocation();
    }

    private void SpawnKeyAtRandomLocation()
    {
        // Verificar que tengamos al menos un punto de aparici�n.
        if (keySpawnPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de aparici�n de la llave asignados.");
            return;
        }

        // Seleccionar aleatoriamente un �ndice entre 0 y el tama�o del arreglo de puntos de aparici�n.
        int randomIndex = Random.Range(0, keySpawnPoints.Length);

        // Instanciar la llave en la posici�n aleatoria.
        Instantiate(keyPrefab, keySpawnPoints[randomIndex].position, Quaternion.identity);
    }


    // make a gizmo to see the spawn points
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;

    //    foreach (Transform spawnPoint in keySpawnPoints)
    //    {
    //        Gizmos.DrawSphere(spawnPoint.position, 0.5f);
    //    }
    //}
}
