using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private EnemyMovement movement;
    private Transform player;
    private float waitTime;
    private float startWaitTime;
    private float stopDistance;

    public void Init(EnemyMovement move, Transform playerRef, float wait, float stopDist)
    {
        movement = move;
        player = playerRef;
        startWaitTime = wait;
        stopDistance = stopDist;
        waitTime = startWaitTime;
    }

    public bool Chase(Vector3 playerPos)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPos);

        // Si el jugador está a más de 3 metros, seguirlo
        if (distanceToPlayer > 3f)
        {
            movement.Move(movement.runSpeed);
            movement.agent.SetDestination(playerPos);
            waitTime = startWaitTime;
            return false;
        }
        else
        {
            // Ya está lo suficientemente cerca, se detiene
            movement.Stop();
            return false; // No vuelve a patrullar aún
        }
    }


    public void ResetChase()
    {
        waitTime = startWaitTime;
    }
}
