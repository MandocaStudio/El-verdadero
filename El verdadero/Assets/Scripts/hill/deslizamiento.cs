
using Unity.Mathematics;
using UnityEngine;

public class deslizamiento : MonoBehaviour
{
    public movimientoPlayer player;

    public float slideForce = 10f;

    public float maxSlideSpeed = 10f;

    public float lateralMovementForce = 10f;



    public void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.canRun = false;
            // player.canMove = false;
            player.dash.canDash = false;

            player.z = 0;


            player.isSliding = true;
            //aceleracion descendente colina
            Vector3 slideDirection = new Vector3(0, 0, 1);
            player.rb.AddForce(slideDirection * slideForce, ForceMode.Acceleration);

            //posibilidad de moverse de forma lateral
            Vector3 lateralMovement = new Vector3(player.x, 0, 0) * lateralMovementForce;
            player.rb.AddForce(lateralMovement, ForceMode.Acceleration);

            //mantener velocidad constante
            if (player.rb.linearVelocity.magnitude > maxSlideSpeed)
            {
                player.rb.linearVelocity = player.rb.linearVelocity.normalized * maxSlideSpeed;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            player.canRun = true;
            player.canMove = true;
            player.dash.canDash = true;

            player.isSliding = false;

        }
    }
}
