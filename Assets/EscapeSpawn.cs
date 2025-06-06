using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeSpawn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CatchObject catchObject = other.GetComponent<CatchObject>();

            if (catchObject != null && catchObject.holdObjectSpawn.transform.childCount > 0)
            {
                Transform heldObject = catchObject.holdObjectSpawn.transform.GetChild(0);
                if (heldObject.gameObject.tag == "SantoGrial")
                {
                    SceneManager.LoadScene("Credits");
                }
                else
                {
                    Debug.Log("No has cogido el Santo Grial, no puedes escapar.");
                }
            }
        }
    }
}
