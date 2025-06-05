using System.Collections;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [Header("References")]
    public Transform[] waypoints;
    public Transform player;

    [Header("Movement Settings")]
    public float waitTime = 4f;
    public float rotateTime = 2f;
    private float stopDistance = 6f;

    private EnemyMovement movement;
    private EnemyChase chase;
    private EnemyVision vision;
    private EnemyShooting shooter;
    private EnemyNoise noise;

    private bool isPatrolling = true;
    private bool playerWasSeen = false;
    private bool isWaitingToPatrol = false;
    private bool isWaitingToNextPoint = false;
    private bool checkingNoise = false; // Para saber si se ha comprobado el ruido
    private bool lastPosPlayerWasChecked = false; // Para saber si se ha comprobado la última posición del jugador

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        chase = GetComponent<EnemyChase>();
        vision = GetComponent<EnemyVision>();
        shooter = GetComponent<EnemyShooting>();
        noise = GetComponent<EnemyNoise>();
    }

    void OnEnable()
    {
        ResumePatrol();
        movement.waypoints = waypoints;
        movement.Init();
        chase.Init(movement, player, waitTime, stopDistance);
    }

    void Update()
    {
        shooter.UpdateCooldown();

        if (noise.CheckForNoise(out Vector3 noisePos) && !playerWasSeen)
        {
            checkingNoise = true;
            isPatrolling = false;
            movement.Move(movement.walkSpeed);
            movement.agent.SetDestination(noisePos);
        }

        if (!vision.SeePlayer(out Vector3 playerPos, transform, out float distance))
        {
            if (playerPos != Vector3.zero && !lastPosPlayerWasChecked)
            {
                movement.agent.SetDestination(playerPos);

                // Esperar 3 segundos después de que desaparezca el ruido antes de volver a patrullar
                if (!isWaitingToPatrol)
                {
                    lastPosPlayerWasChecked = true;
                    StartCoroutine(WaitAndResumePatrol());
                }
                return;
            }
            if (isPatrolling && !checkingNoise)
            {
                HandlePatrol();
            }
        }
        else
        {
            Debug.Log("Player seen");
            lastPosPlayerWasChecked = false;
            isPatrolling = false;
            playerWasSeen = true;

            // Lo sigue hasta 3 metros
            chase.Chase(playerPos);
            vision.HuntPlayer();

            // Dispara si está a distancia
            if (distance <= 6f)
            {
                shooter.TryShoot(player);
            }
        }
    }

    void HandlePatrol()
    {
        if (movement.IsAtDestination())
        {
            if (!isWaitingToNextPoint)
            {
                StartCoroutine(WaitAtWaypoint());
            }
        }
        else
        {
            movement.Move(movement.walkSpeed);
        }
    }

    private IEnumerator WaitAndResumePatrol()
    {
        isWaitingToPatrol = true;
        yield return new WaitForSeconds(3f);

        ResumePatrol();

        isWaitingToPatrol = false;
    }

    public void ResumePatrol()
    {
        Debug.Log("Resuming patrol");
        isPatrolling = true;
        playerWasSeen = false;
        checkingNoise = false;
        vision.ResetHunt();
        chase.ResetChase();
        movement.GoToCurrentWaypoint();
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaitingToNextPoint = true;
        yield return new WaitForSeconds(3f);

        // Avanza al siguiente punto de patrulla
        movement.NextPoint();

        isWaitingToNextPoint = false;
    }
}
