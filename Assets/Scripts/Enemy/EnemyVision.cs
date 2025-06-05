using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private float viewRadius = 10f;
    private float viewAngle = 180f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private Vector3 lastPlayerPosition;

    void Start()
    {
        targetMask |= 1 << LayerMask.NameToLayer("Player");
    }

    public bool SeePlayer(out Vector3 playerPosition, Transform self, out float distanceToPlayer)
    {
        Collider[] targets = Physics.OverlapSphere(self.position, viewRadius, targetMask);
        foreach (Collider col in targets)
        {
            Vector3 dirToTarget = (col.transform.position - self.position).normalized;
            distanceToPlayer = Vector3.Distance(self.position, col.transform.position);

            if (Vector3.Angle(self.forward, dirToTarget) < viewAngle / 2 &&
                !Physics.Raycast(self.position, dirToTarget, distanceToPlayer, obstacleMask))
            {
                lastPlayerPosition = col.transform.position;
                playerPosition = col.transform.position;
                return true;
            }
        }

        playerPosition = lastPlayerPosition;
        distanceToPlayer = 0f;
        return false;
    }

    public void HuntPlayer()
    {
        targetMask |= 1 << LayerMask.NameToLayer("HiddenByShadow");
    }

    public void ResetHunt()
    {
        targetMask &= ~(1 << LayerMask.NameToLayer("HiddenByShadow"));
    }

    void OnDisable()
    {
        // Reset the last player position when the script is disabled
        lastPlayerPosition = Vector3.zero;
    }
}
