using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchObject : MonoBehaviour
{
     public LayerMask throwableLayer;
    public float range = 1f;

    private RaycastHit topRayHitInfo;
    private bool isRightClickPressed = false; // Estado para rastrear el clic derecho

    public GameObject holdObjectSpawn;

    void Update()
    {
        // Raycast desde los pies
        Vector3 originFeet = transform.position - new Vector3(0, 0.7f, 0);
        Ray rayFeet = new Ray(originFeet, transform.forward);

        // Raycast desde el centro
        Vector3 originCenter = transform.position;
        Ray rayCenter = new Ray(originCenter, transform.forward);

        Debug.DrawRay(originFeet, transform.forward * range, Color.red, 1f);
        Debug.DrawRay(originCenter, transform.forward * range, Color.blue, 1f);

        bool hitFeet = Physics.Raycast(rayFeet, out topRayHitInfo, range, throwableLayer);
        bool hitCenter = Physics.Raycast(rayCenter, out RaycastHit centerRayHitInfo, range, throwableLayer);

        // Si cualquiera de los dos raycasts detecta un objeto
        if ((hitFeet || hitCenter))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Usar el hit más cercano (prioridad al de los pies)
                RaycastHit hitInfo = hitFeet ? topRayHitInfo : centerRayHitInfo;
                IObjectCatchable objectCatchable = hitInfo.transform.gameObject.GetComponent<IObjectCatchable>();
                Rigidbody rbHeldObject = hitInfo.transform.GetComponent<Rigidbody>();
                if (rbHeldObject != null)
                {
                    rbHeldObject.useGravity = false;
                    rbHeldObject.isKinematic = true;
                }
                if (objectCatchable != null)
                {
                    objectCatchable.HoldObject(holdObjectSpawn);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && holdObjectSpawn.transform.childCount > 0)
        {
            DropObj();
        }
        if (Input.GetMouseButtonDown(1)) // Botón derecho del ratón
        {
            isRightClickPressed = true;
        }
        else if (Input.GetMouseButtonUp(1)) // Botón derecho del ratón
        {
            isRightClickPressed = false;
        }

        if (isRightClickPressed && Input.GetMouseButtonDown(0)) // Botón izquierdo del ratón
        {
            ThrowObj();
            isRightClickPressed = false; // Reiniciar el estado
        }
    }

    void CatchObj()
    {
        IObjectCatchable objectCatchable = topRayHitInfo.transform.gameObject.GetComponent<IObjectCatchable>();
        Rigidbody rbHeldObject = topRayHitInfo.transform.GetComponent<Rigidbody>();
        rbHeldObject.useGravity = false;
        rbHeldObject.isKinematic = true;
        if (objectCatchable != null)
        {
            objectCatchable.HoldObject(holdObjectSpawn);
        }
    }

    void DropObj()
    {
        Transform heldObject = holdObjectSpawn.transform.GetChild(0);
        heldObject.SetParent(null);

        Rigidbody rbHeldObject = heldObject.GetComponent<Rigidbody>();
        rbHeldObject.useGravity = true;
        rbHeldObject.isKinematic = false;

    }
    
    void ThrowObj()
    {
        if (holdObjectSpawn.transform.childCount == 0) return;
        Transform heldObject = holdObjectSpawn.transform.GetChild(0);
        if (heldObject.tag == "Throwable")
        {
            heldObject.SetParent(null);

            Rigidbody rbHeldObject = heldObject.GetComponent<Rigidbody>();
            rbHeldObject.useGravity = true;
            rbHeldObject.isKinematic = false;

            rbHeldObject.AddForce(holdObjectSpawn.transform.forward * 10f, ForceMode.Impulse);
        }
    }
}
