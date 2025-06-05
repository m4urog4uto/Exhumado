using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject groupEnemy;

    private bool hasPlayerExited = false;

    public bool HasPlayerExited
    {
        get { return hasPlayerExited; }
        set { hasPlayerExited = value; }
    }

    void Start()
    {
        groupEnemy.SetActive(false); // Asegurarse de que el grupo de enemigos esté desactivado al inicio
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactivar el grupo de enemigosf al salir del checkpoint
            if (groupEnemy != null)
            {
                groupEnemy.SetActive(true);
                hasPlayerExited = true; // Marcar que el jugador ha salido del checkpoint
            }
        }
    }

    public void ResetSection()
    {
        if (groupEnemy != null)
        {
            groupEnemy.SetActive(false);
        }
    }
}
