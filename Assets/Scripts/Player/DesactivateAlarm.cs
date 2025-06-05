using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateAlarm : MonoBehaviour
{
    public Alarm alarm;
    [SerializeField] OpenDoor openDoorScript; // Referencia al script OpenDoor
    [SerializeField] SantoGrialProtection santoGrialProtection; // Referencia al script SantoGrialProtection
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameObject.CompareTag("Noise"))
        {
            alarm.IsActiveAlarm = false;
            openDoorScript.HasAlarm = false; // Desactivar la alarma en el script OpenDoor
            santoGrialProtection.HasAlarm = false; // Desactivar la alarma en el script SantoGrialProtection
        }
        else if (other.CompareTag("Enemy"))
        {
            // Si el objeto que entra es un enemigo, también desactivar la alarma
            alarm.IsActiveAlarm = false;
        }
    }
}
