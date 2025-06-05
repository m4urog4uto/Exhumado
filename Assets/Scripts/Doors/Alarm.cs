using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private bool isActiveAlarm;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Transform noisePosition;

    private bool isNoiseCreated = false;

    private Renderer rend;

    public bool IsActiveAlarm
    {
        get { return isActiveAlarm; }
        set
        {
            isActiveAlarm = value;
            UpdateAlarmMaterial();
        }
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateAlarmMaterial();
    }

    void Update()
    {
        UpdateAlarmMaterial();

        if (isActiveAlarm && !isNoiseCreated)
        {
            // Crear un GameObject vacío con el tag "Noise" en la posición de la colisión
            GameObject noise = new GameObject("Noise");
            noise.transform.SetParent(noisePosition);
            noise.tag = "Noise";
            noise.transform.position = noisePosition.position;

            // Agregar un BoxCollider y configurarlo como trigger
            BoxCollider box = noise.AddComponent<BoxCollider>();
            box.isTrigger = true;

            DesactivateAlarm desactivate = noise.AddComponent<DesactivateAlarm>();
            desactivate.alarm = this;

            isNoiseCreated = true; // Marcar que el ruido ha sido creado
        }

        if (!isActiveAlarm)
        {
            isNoiseCreated = false; // Reiniciar el estado de ruido si la alarma no está activa
        }
    }

    void UpdateAlarmMaterial()
    {
        if (rend != null)
        {
            rend.material = isActiveAlarm ? activeMaterial : inactiveMaterial;
        }
    }
}
