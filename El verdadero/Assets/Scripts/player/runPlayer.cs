using System.Collections;
using UnityEngine;

public class runPlayer : MonoBehaviour
{
    [Header("variables correr")]


    public float runSpeed = 3f;
    public float acelerationSpeed = 3f;

    [SerializeField] private float baseSpeed = 6f;

    public movimientoPlayer movimiento;

    public bool isRuning = false;

    [Header("variables planeo")]
    public float glideGravity = 0;

    public void run()
    {
        if (Input.GetButtonDown("Run") && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            if (movimiento.jump.isJumping == false)
            {
                movimiento.irisAnimation.Play("run");

            }


            isRuning = true;
            if (Input.GetButton("Run") && movimiento.speed <= 30 && isRuning == true)
            {
                StartCoroutine(aceleracion());
            }
        }
        else if (Input.GetButtonUp("Run") || (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0))
        {
            movimiento.speed = baseSpeed;
            isRuning = false;

            StopCoroutine(aceleracion());
        }
    }

    IEnumerator aceleracion()
    {
        if (movimiento.speed <= 9)
        {
            movimiento.speed += runSpeed;
        }

        if (movimiento.amarillo == true)
        {
            yield return new WaitForSeconds(3f);

            while (Input.GetButton("Run"))
            {

                yield return new WaitForSeconds(1.5f);

                if (movimiento.speed + runSpeed < 30 && isRuning == true)
                {
                    movimiento.speed += runSpeed;

                }

                else if (movimiento.speed + runSpeed >= 30 && isRuning == true)
                {
                    movimiento.speed = 30;
                    break;
                }

            }
        }


    }

}
