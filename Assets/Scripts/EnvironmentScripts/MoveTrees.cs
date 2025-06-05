using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrees : MonoBehaviour
{
    public float velocidad = 10f;

    void Update()
    {
        transform.position += Vector3.right * velocidad * Time.deltaTime;
    }
}
