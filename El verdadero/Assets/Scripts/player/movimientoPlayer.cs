using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoPlayer : MonoBehaviour
{
    [Header("colors")]
    public menuRadialController menuRadial;
    [Header("Move Variables")]
    public float speed = 8;
    public float rotationSpeed = 200.0f;
    public float x, z;

    public Vector3 movement;

    public bool canMove = true;

    [Header("variables de salto")]
    public bool isGrounded = true;

    public int groundContactCount = 0;

    public jumpPlayer jump;

    [Header("variables de correr")]

    public bool canRun = true;

    public runPlayer runing;

    [Header("variables de dash")]
    public dashPlayer dash;

    [Header("variables de color")]
    public bool azul = true;
    public bool rojo = true;
    public bool amarillo = false;
    public bool magenta = true;
    public bool cyan = true;
    public bool verde = true;

    public Rigidbody rb;

    [Header("variables de culatazo")]
    public culatazoPlayer culatazo;

    [Header("attack")]
    public GameObject attackContainer;

    [Header("animaciones")]
    public GameObject irisModel;
    public Animation irisAnimation;


    [Header("take Damage")]
    public health healthPlayer;

    [Header("sliding variables")]
    public bool isSliding;

    [Header("deathScream")]

    public PanelMuerte PanelMuerte;

    [Header("Sonido")]

    public AudioSource soundtrack;

    public AudioClip combatMusic;

    public AudioClip chil;

    public pasos PlayerPasos;

    [Header("Hud")]

    public GameObject hud;

    void Update()
    {

        x = Input.GetAxis("Horizontal");

        if (isSliding == false)
        {
            z = Input.GetAxis("Vertical");

        }

        movement = new Vector3(x, 0, z).normalized * speed * Time.deltaTime;
        if (canMove == true && dash.isDashing == false)
        {
            playerMovement();

            if ((x != 0 || z != 0) && jump.isJumping == false && runing.isRuning == false && dash.isDashing == false)
            {
                irisAnimation.Stop("IdleBrush");

                irisAnimation.Play("RunBrush");


                // irisAnimation.Play("rig|jogging");

            }
            else if (x == 0 && z == 0 && jump.isJumping == false && runing.isRuning == false && dash.isDashing == false)
            {
                irisAnimation.Stop("RunBrush");



                irisAnimation.Play("IdleBrush");
                // irisAnimation.Stop("jogging");

            }
        }





        // if (menuRadial.colorActivated == 1)
        // {
        //     jump.planear();
        // }


        //para despues de la jam
        // if (isGrounded == true)
        // {
        //     runing.run();

        // }

        if (dash.canDash == false && runing.isRuning == false && azul == true || Input.GetButton("Run") && dash.dashCooldownTimer != 0)
        {
            dash.dashCooldownTimer += Time.deltaTime;
            if (dash.dashCooldownTimer >= dash.dashCooldown)
            {
                dash.canDash = true;
                dash.dashCooldownTimer = 0;
            }
        }

        if (Input.GetButtonDown("Dash") && dash.canDash && azul)
        {
            StartCoroutine(dash.dashAction());
        }

        //culatazo.culatazo();

    }

    private void FixedUpdate()
    {

    }
    public void playerMovement()
    {

        Vector3 remappedMovement = new Vector3(movement.z, 0, -movement.x);

        transform.Translate(remappedMovement, Space.World);

        if (remappedMovement != Vector3.zero)
        {
            // Calcular el ángulo objetivo basado en la dirección de movimiento
            float targetAngle = Mathf.Atan2(remappedMovement.x, remappedMovement.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            if (!isSliding)
            {
                irisModel.transform.rotation = Quaternion.Slerp(irisModel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            attackContainer.transform.rotation = Quaternion.Slerp(irisModel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("floor"))
        {
            groundContactCount++;
            if (groundContactCount == 1)
            {
                speed = 8;

                isGrounded = true;

                jump.canJump = true;
                culatazo.canFall = false;
                jump.isJumping = false;
                irisAnimation.Stop("MidJump");
                irisAnimation.Play("IdleBrush");
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag.Contains("floor"))
        {

            groundContactCount--;
            if (groundContactCount == 0)
            {
                isGrounded = false;
                jump.canJump = false;
                culatazo.canFall = true;
            }
        }
    }

    IEnumerator canJumpCooldown()
    {
        Debug.Log("entro");
        yield return new WaitForSeconds(0.2f);
        jump.canJump = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Contains("floor"))
        {
            isGrounded = true;
            jump.canJump = true;

            culatazo.canFall = false;
        }
    }
}
