using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantoGrial : MonoBehaviour, IObjectCatchable
{
    [SerializeField] private SantoGrialProtection santoGrialProtection;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("SantoGrialProtected");
    }

    void Update()
    {
        if (!santoGrialProtection.HasAlarm)
        {
            gameObject.layer = LayerMask.NameToLayer("SantoGrial");
        }
    }
    public void HoldObject(GameObject holdObjectSpawn)
    {
        transform.SetParent(holdObjectSpawn.transform);
        transform.position = holdObjectSpawn.transform.position;
        transform.rotation = holdObjectSpawn.transform.rotation;
    }
}
