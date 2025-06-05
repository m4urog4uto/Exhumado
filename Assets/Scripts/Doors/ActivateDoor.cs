using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] OpenDoor openDoorScript; // Referencia al script OpenDoor
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openDoorScript.IsActiveDoor = true; // Activar la puerta
        }
    }
}
