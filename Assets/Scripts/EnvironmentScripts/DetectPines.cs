using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPines : MonoBehaviour
{
    [SerializeField] GameObject pinesPrefab;
    [SerializeField] Transform pinesSpawns;

    void Start()
    {
        Vector3 spawnPosition = pinesSpawns.position + pinesSpawns.right * 200f;
        Instantiate(pinesPrefab, spawnPosition, pinesSpawns.rotation);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            if (other.gameObject.tag == "DetectionFront")
            {
                Instantiate(pinesPrefab, pinesSpawns.position, pinesSpawns.rotation);
            }
            else if (other.gameObject.tag == "DetectionEnd")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
