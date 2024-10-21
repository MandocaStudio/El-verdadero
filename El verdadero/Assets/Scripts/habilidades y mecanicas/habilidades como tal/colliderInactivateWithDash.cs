using UnityEngine;

public class ColliderInactivateWithDash : MonoBehaviour
{
    public Collider colliderObject;
    private movimientoPlayer player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<movimientoPlayer>();

            if (player != null && player.dash.isDashing)
            {
                colliderObject.enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<movimientoPlayer>();

            if (player != null && player.dash.isDashing)
            {
                colliderObject.enabled = false;
            }
            else if (player != null && !player.dash.isDashing)
            {
                colliderObject.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && player != null)
        {
            colliderObject.enabled = true;
            player = null;
        }
    }
}
