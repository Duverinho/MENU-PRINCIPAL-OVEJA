using UnityEngine;
using System.Collections; // Para usar corrutinas

[RequireComponent(typeof(Rigidbody))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrullaje")]
    public Transform[] patrolPoints;
    public float moveSpeed = 3f;
    private int currentPointIndex = 0;

    [Header("Detección")]
    public float detectionRadius = 5f;
    public string playerTag = "Player";
    public float lostPlayerDelay = 3f; // Espera antes de volver a patrullar

    [Header("Empuje")]
    public float pushForce = 15f; // Mayor fuerza de empuje
    public float pushCooldown = 2f;
    private float lastPushTime = -999f;

    private Rigidbody rb;
    private GameObject player;
    private bool isChasing = false;
    private Coroutine lostPlayerCoroutine; // Para manejar el retorno a patrulla

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void FixedUpdate()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
            if (!isChasing)
            {
                isChasing = true;
                if (lostPlayerCoroutine != null) StopCoroutine(lostPlayerCoroutine); // Cancela el retorno si vuelve a ver al jugador
            }
        }
        else if (isChasing && lostPlayerCoroutine == null)
        {
            lostPlayerCoroutine = StartCoroutine(ReturnToPatrolAfterDelay());
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length < 2) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(targetPoint.position.x, currentPosition.y, targetPoint.position.z);

        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(player.transform.position.x, currentPosition.y, player.transform.position.z);

        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        float distanceToPlayer = Vector3.Distance(currentPosition, player.transform.position);
        if (distanceToPlayer < 1.5f && Time.time >= lastPushTime + pushCooldown)
        {
            PushPlayer();
        }
    }

    void PushPlayer()
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 pushDir = (player.transform.position - transform.position).normalized;
            pushDir.y = 0.5f; // Añadimos impulso vertical

            playerRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            lastPushTime = Time.time;
        }
    }

    IEnumerator ReturnToPatrolAfterDelay()
    {
        yield return new WaitForSeconds(lostPlayerDelay);
        isChasing = false;
        lostPlayerCoroutine = null; // Reiniciar la variable
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
