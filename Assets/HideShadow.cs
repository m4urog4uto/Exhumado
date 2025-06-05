using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShadow : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("HiddenByShadow");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           other.gameObject.layer = LayerMask.NameToLayer("Player");
        }   
    }
}
