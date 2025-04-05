using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrullaje")]
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;

    [Header("Detección")]
    public float detectionRadius = 5f;
    public string playerTag = "Player";
    public float lostPlayerDelay = 3f;

    [Header("Empuje")]
    public float pushForce = 15f;
    public float pushCooldown = 2f;
    private float lastPushTime = -999f;

    private NavMeshAgent agent;
    private GameObject player;
    private bool isChasing = false;
    private Coroutine lostPlayerCoroutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
            if (!isChasing)
            {
                isChasing = true;
                if (lostPlayerCoroutine != null) StopCoroutine(lostPlayerCoroutine);
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
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
        else if (!agent.hasPath)
        {
            agent.SetDestination(targetPoint.position);
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position);

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < 1.5f && Time.time >= lastPushTime + pushCooldown)
            {
                PushPlayer();
            }
        }
    }

    void PushPlayer()
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 pushDir = (player.transform.position - transform.position).normalized;
            pushDir.y = 0.5f;
            playerRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            lastPushTime = Time.time;
        }
    }

    IEnumerator ReturnToPatrolAfterDelay()
    {
        yield return new WaitForSeconds(lostPlayerDelay);
        isChasing = false;
        lostPlayerCoroutine = null;
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
