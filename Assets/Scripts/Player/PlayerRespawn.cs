using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    Stack<GameObject> respawnPositions = new Stack<GameObject>();
    void Start()
    {
        GameObject point = GameObject.Find("RespawnPoint");
        if (point != null)
        {
            respawnPositions.Push(point);
        }
    }

    public void Respawn()
    {
        CharacterController cc = GetComponent<CharacterController>();
        GameObject respawn = respawnPositions.Peek();
        cc.enabled = false;
        transform.position = respawn.gameObject.transform.position;
        cc.enabled = true;

        // Solo cambiar el último checkpoint
        Checkpoint lastCheckpoint = respawn.GetComponent<Checkpoint>();
        if (lastCheckpoint != null)
        {
            lastCheckpoint.ResetSection();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "CheckPoint")
        {
            GameObject checkPoint = other.gameObject;
            respawnPositions.Push(checkPoint);
        }
    }
}
