using UnityEngine;

public class TurningController : MonoBehaviour
{
    [SerializeField] LayerMask floorMask;
    float camRayLenght = 100f;

    Ray camRay;
    RaycastHit raycastHit;
    Vector3 playerToMouse;

    void FixedUpdate()
    {
        Turning();
    }

    void Turning()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out raycastHit, camRayLenght, floorMask))
        {
            // if (raycastHit.collider.CompareTag("Enemy") || raycastHit.collider.CompareTag("RadioEnemy"))
            // {
            //     Vector3 direction = (raycastHit.transform.position - transform.position).normalized;
            //     Quaternion lookRotation = Quaternion.LookRotation(direction);
            //     transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y - 28f, 0f);
            // }
            // else
            // {
            playerToMouse = raycastHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse, Vector3.up);
            transform.rotation = newRotation;
            // }

        }
    }
}
