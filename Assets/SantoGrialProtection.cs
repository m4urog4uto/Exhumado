using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantoGrialProtection : MonoBehaviour
{
    [SerializeField] private Alarm alarm;
    private bool hasAlarm = true;
    public bool HasAlarm
    {
        get { return hasAlarm; }
        set { hasAlarm = value; }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasAlarm)
            {
                Debug.Log("Obtener Santo Grial");
            }

            if (hasAlarm)
            {
                alarm.IsActiveAlarm = true; // Activar la alarma
            }
        }
    }
}
