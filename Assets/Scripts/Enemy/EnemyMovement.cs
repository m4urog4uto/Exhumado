using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public float walkSpeed = 6;
    public float runSpeed = 9;
    public float startWaitTime = 4;
    public float timeToRotate = 2;

    private int currentWaypointIndex = 0;
    private float waitTime;
    private float rotateTime;

    public void Init()
    {
        waitTime = startWaitTime;
        rotateTime = timeToRotate;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void Move(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }

    public void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void GoToCurrentWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public bool IsAtDestination() => agent.remainingDistance <= agent.stoppingDistance;
}