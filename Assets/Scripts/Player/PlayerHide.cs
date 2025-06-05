using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public HidingSpot nearHidingSpot;
    public GameObject player;
    public bool isHiding = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearHidingSpot != null && !isHiding)
        {
            EnterHidingSpot(nearHidingSpot);
        }
        else if (Input.GetKeyDown(KeyCode.E) && isHiding)
        {
            ExitHidingSpot();
        }
    }

    void EnterHidingSpot(HidingSpot spot)
    {
        isHiding = true;
        transform.position = spot.GetHidingPosition();
        PlayerBase playerMove = player.GetComponent<PlayerBase>();
        playerMove.enabled = false; // Desactiva el movimiento del jugador
    }

    void ExitHidingSpot()
    {
        isHiding = false;
        PlayerBase playerMove = player.GetComponent<PlayerBase>();
        playerMove.enabled = true; // Desactiva el movimiento del jugador
        // Rehabilita movimiento, vuelve a posición anterior, etc.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HiddingSpot"))
        {
            nearHidingSpot = other.GetComponent<HidingSpot>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HiddingSpot"))
        {
            if (nearHidingSpot == other.GetComponent<HidingSpot>())
                nearHidingSpot = null;
        }
    }
}
