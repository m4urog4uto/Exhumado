using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    public float shootCooldown = 2f;
    public float shootRange = 10f;

    private float shootTimer;

    public void UpdateCooldown()
    {
        shootTimer -= Time.deltaTime;
    }

    public void TryShoot(Transform target)
    {
        if (shootTimer > 0f) return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > shootRange) return;

        Vector3 dir = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(dir).eulerAngles.y, 0f);

        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        shootTimer = shootCooldown;
    }
}
