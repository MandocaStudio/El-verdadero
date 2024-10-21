using UnityEngine;

public class canMove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movimientoPlayer player = other.GetComponent<movimientoPlayer>();
            dashPlayer playerDash = other.GetComponent<dashPlayer>();

            playerDash.enabled = true;
            player.enabled = true;
        }
    }
}
