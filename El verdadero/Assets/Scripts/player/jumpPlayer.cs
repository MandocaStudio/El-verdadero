using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class jumpPlayer : MonoBehaviour
{
    public float jumpForce = 12f;
    public float secondJumpForce = 5f;
    public bool doubleJump = false;
    public float jumpCount = 0;

    public movimientoPlayer movimiento;

    [Header("Gliding variables")]
    public bool canGlide;
    public float normalDrag = 0f;
    public float glidingDrag = 7f;

    [Header("Animacion")]
    public bool isJumping;

    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 2f;

    public void jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (movimiento.isGrounded == true)
            {
                movimiento.speed = 8f;

                isJumping = true;
                canGlide = false;

                movimiento.irisAnimation.Play("JumpBrush");


                movimiento.rb.linearVelocity = new Vector3(movimiento.rb.linearVelocity.x, jumpForce, movimiento.rb.linearVelocity.z);

                // if (doubleJump == true && movimiento.verde == true)
                // {
                //     jumpCount = 1;
                // }

                movimiento.isGrounded = false;

            }
            // doble salto
            // else if (jumpCount == 1 && movimiento.verde == true && Input.GetButtonDown("Jump"))
            // {
            //     movimiento.irisAnimation["JumpBrush"].time = 0;
            //     movimiento.irisAnimation.Play("JumpBrush");
            //     movimiento.rb.linearVelocity = new Vector3(movimiento.rb.linearVelocity.x, secondJumpForce, movimiento.rb.linearVelocity.z);

            //     jumpCount = 0;

            //     StartCoroutine(EnableGlideAfterDelay(0.2f));
            // }


        }
    }


    // planear
    // public void planear()
    // {
    //     if (Input.GetButton("Jump") && movimiento.verde == true && canGlide == true)
    //     {
    //         movimiento.rb.linearDamping = glidingDrag;
    //         Debug.Log("cayendo");
    //     }
    //     else if (Input.GetButtonUp("Jump") && movimiento.verde == true)
    //     {
    //         movimiento.rb.linearDamping = normalDrag;
    //     }
    // }

    // IEnumerator EnableGlideAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     canGlide = true;
    // }

}
