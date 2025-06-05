using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Transform hidingPosition;

    public Vector3 GetHidingPosition()
    {
        return hidingPosition.position;
    }
}
