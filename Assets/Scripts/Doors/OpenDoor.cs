using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private GameObject door; // Referencia al objeto de la puerta
    [SerializeField] private bool isActiveDoor;
    [SerializeField] private bool hasAlarm;
    [SerializeField] private bool isBigDoor;

    public bool HasAlarm
    {
        get { return hasAlarm; }
        set { hasAlarm = value; }
    }

    [SerializeField] private Alarm alarm; // Referencia al script de alarma

    public bool IsActiveDoor
    {
        get { return isActiveDoor; }
        set { isActiveDoor = value; UpdateStatusColor(); }
    }

    private Vector3 originalPosition;
    private Renderer statusRenderer;

    void Start()
    {
        originalPosition = door.transform.position;

        // Buscar el hijo llamado "Status" y guardar su Renderer
        Transform statusChild = door.transform.Find("Status");
        if (statusChild != null)
        {
            statusRenderer = statusChild.GetComponent<Renderer>();
            UpdateStatusColor();
        }
    }

    void Update()
    {
        UpdateStatusColor();
    }

    void UpdateStatusColor()
    {
        if (statusRenderer != null)
        {
            statusRenderer.material.color = isActiveDoor ? Color.green : Color.red;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActiveDoor && !hasAlarm)
            {
                StartCoroutine(MoveDoor());
            }

            if (hasAlarm)
            {
                alarm.IsActiveAlarm = true; // Activar la alarma
            }
        }
    }

    IEnumerator MoveDoor()
    {
        if (!isBigDoor)
        {
            // Mover la puerta 3 metros hacia la flecha azul (eje Z local)
            Vector3 targetPosition = originalPosition + door.transform.forward * -3f;
            door.transform.position = targetPosition;
        }
        else
        {
            Vector3 targetPosition = originalPosition + door.transform.right * -3f;
            door.transform.position = targetPosition;
        }

        yield return new WaitForSeconds(5f);

        // Volver a la posición original
        door.transform.position = originalPosition;
    }
}
