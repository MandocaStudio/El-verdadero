
using UnityEngine;

public class sliding : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public movimientoPlayer player;

    public float slideForce = 10f;

    public float maxSlideSpeed = 10f;

    public float lateralMovementForce = 10f;



    public void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("hillx"))
        {

            Vector3 slideDirection = new Vector3(0, 0, 1);
            slidingAction(player.x, 0, slideDirection);
        }

        if (other.gameObject.CompareTag("hillz"))
        {
            Vector3 slideDirection = new Vector3(-1, 0, 0);
            slidingAction(0, player.z, slideDirection);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("hillx") || other.collider.CompareTag("hillz"))
        {
            player.canRun = true;
            player.canMove = true;
            player.dash.canDash = true;

            player.isSliding = false;
        }
    }

    public void slidingAction(float x, float z, Vector3 slideDirection)
    {
        player.canRun = false;
        // player.canMove = false;
        player.dash.canDash = false;

        player.isSliding = true;
        //aceleracion descendente colina


        player.rb.AddForce(slideDirection * slideForce, ForceMode.Acceleration);

        //posibilidad de moverse de forma lateral
        Vector3 lateralMovement = new Vector3(x, 0, z) * lateralMovementForce;
        player.rb.AddForce(lateralMovement, ForceMode.Acceleration);

        //mantener velocidad constante
        if (player.rb.linearVelocity.magnitude > maxSlideSpeed)
        {
            player.rb.linearVelocity = player.rb.linearVelocity.normalized * maxSlideSpeed;
        }
    }
}


