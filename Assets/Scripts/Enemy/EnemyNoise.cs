using System.Collections;
using UnityEngine;

public class EnemyNoise : MonoBehaviour
{
    private GameObject currentNoise;

    private PatrollingEnemy patrollingEnemy;

    void Start()
    {
        patrollingEnemy = GetComponent<PatrollingEnemy>();
    }

    public bool CheckForNoise(out Vector3 noisePos)
    {
        if (currentNoise != null)
        {
            noisePos = currentNoise.transform.position;
            return true;
        }

        var noise = GameObject.FindWithTag("Noise");
        if (noise != null)
        {
            currentNoise = noise;
            noisePos = noise.transform.position;
            return true;
        }
        
        patrollingEnemy.ResumePatrol();
        noisePos = Vector3.zero;
        return false;
    }

    public void ClearNoise()
    {
        currentNoise = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Noise"))
        {
            Destroy(other.gameObject);
        }
    }
}
