using UnityEngine;

public struct SoundData
{
    public Vector3 position;    // Posici�n del emisor
    public float volume;        // Volumen base
    public string soundType;    // Tipo de sonido
    public float frequency;     // Frecuencia del sonido (agudo/grave)
    public LayerMask layers;    // Capas que puede atravesar
    public float duration;      // Duraci�n del sonido (0 para instant�neo)
    public object sender;       // Referencia al emisor (opcional)
}
