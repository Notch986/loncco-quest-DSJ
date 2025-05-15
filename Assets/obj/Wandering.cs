using UnityEngine;

public class Wander : MonoBehaviour
{
    public float maxSpeed = 0.3f;                   // Velocidad m�xima
    public float maxForce = 0.0004f;                 // Fuerza m�xima
    public float wanderRingDistance = 1.18f;         // Distancia al c�rculo de deambulaci�n
    public float wanderRingRadius = 0.53f;           // Radio del c�rculo de deambulaci�n
    public bool useImprovedWander = true;         // Alterna entre wandering b�sico y mejorado

    private Vector3 velocity;                     // Velocidad actual
    private Vector3 wanderTarget;                 // Objetivo para el wandering b�sico
    public float wanderUpdateTime = 2f;           // Tiempo entre actualizaciones del wander
    private float timeSinceLastWander;            // Tiempo desde la �ltima actualizaci�n del wander
    private Vector3 improvedWanderTarget;         // Objetivo del wandering mejorado

    void Start()
    {
        // Inicializar wander target
        timeSinceLastWander = wanderUpdateTime;

        // Inicializar improvedWanderTarget en vector 0
        improvedWanderTarget = Vector3.zero;
    }

    void Update()
    {
        timeSinceLastWander += Time.deltaTime;

        if (useImprovedWander)
        {
            // Wander mejorado
            if (timeSinceLastWander > wanderUpdateTime)
            {
                timeSinceLastWander = 0f;
                improvedWanderTarget =  GetNewImprovedWanderTarget(); // Actualiza el objetivo
            }
            velocity += SeekTarget(transform.position + improvedWanderTarget);
        }
        else
        {
            // Wander b�sico
            if (timeSinceLastWander > wanderUpdateTime)
            {
                timeSinceLastWander = 0f;
                wanderTarget = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            }
            velocity += SeekTarget(wanderTarget);
        }

        // Limita la velocidad a la velocidad m�xima
        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        // Restringir el movimiento solo al plano XZ (ignorar Y)
        velocity.y = 0;

        // Actualiza la posici�n
        transform.position += velocity * Time.deltaTime;

        // Gira el objeto hacia la direcci�n del movimiento, solo en el plano XZ
        if (velocity != Vector3.zero)
        {
            Vector3 lookDir = new Vector3(velocity.x, 0, velocity.z);  // Solo XZ
            transform.forward = lookDir.normalized;
        }
    }

    Vector3 SeekTarget(Vector3 targetPos)
    {
        // Comportamiento Seek para deambular hacia un objetivo
        Vector3 desired = (targetPos - transform.position).normalized * maxSpeed;
        Vector3 steer = desired - velocity;

        if (steer.magnitude > maxForce)
            steer = steer.normalized * maxForce;

        return steer;
    }

    Vector3 GetNewImprovedWanderTarget()
    {
        // Proyecta un punto en el futuro seg�n la velocidad actual
        Vector3 futurePos = new Vector3(velocity.x, 0, velocity.z).normalized * wanderRingDistance;

        // Desplaza un objetivo dentro del c�rculo alrededor del punto futuro
        float randomAngle = Random.Range(0f, 360f);
        Vector3 displacement = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)) * wanderRingRadius;
        return futurePos + displacement;
    }

    void OnDrawGizmos()
    {
        // Dibuja una l�nea verde representando la velocidad
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + velocity);

        if (useImprovedWander)
        {
            // Dibuja el c�rculo de wandering mejorado
            Vector3 futurePos = transform.position + new Vector3(velocity.x, 0, velocity.z).normalized * wanderRingDistance;
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(futurePos, wanderRingRadius);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(futurePos, transform.position + improvedWanderTarget); // Dibuja una l�nea hacia el objetivo actual
        }
        else
        {
            // Dibuja el objetivo aleatorio en wandering b�sico
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(wanderTarget, 0.2f);
        }
    }
}
