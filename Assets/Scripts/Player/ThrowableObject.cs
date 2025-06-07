using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour, IObjectCatchable
{
    public void HoldObject(GameObject holdObjectSpawn)
    {
        Debug.Log("Objeto capturado: " + gameObject.name);
        transform.SetParent(holdObjectSpawn.transform);
        transform.position = holdObjectSpawn.transform.position;
        transform.rotation = holdObjectSpawn.transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Enemy"))
        {
            // Eliminar todos los objetos "Noise" existentes antes de crear uno nuevo
            GameObject[] noises = GameObject.FindGameObjectsWithTag("Noise");
            foreach (GameObject n in noises)
            {
                Destroy(n);
            }

            // Crear un GameObject vacío con el tag "Noise" en la posición de la colisión
            GameObject noise = new GameObject("Noise");
            noise.tag = "Noise";
            noise.transform.position = collision.contacts[0].point;

            // Agregar un BoxCollider y configurarlo como trigger
            BoxCollider box = noise.AddComponent<BoxCollider>();
            box.isTrigger = true;

            Destroy(noise, 5f); // Destruir después de 5 segundos
        }
    }
}
