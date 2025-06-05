using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupEnemy : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>();

    void Awake()
    {
        // Buscar nietos con tag "Enemy"
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child)
            {
                if (grandchild.CompareTag("Enemy"))
                {
                    initialPositions[grandchild.gameObject] = grandchild.position;
                }
            }
        }
    }

   void OnDisable()
    {
        // Restaurar posiciones iniciales cuando el padre se deshabilita
        foreach (var kvp in initialPositions)
        {
            kvp.Key.transform.position = kvp.Value;
            kvp.Key.SetActive(true); // O false si quieres desactivarlos
        }
    }
}
