using System.Collections;
using UnityEngine;

public class jumpPlayer : MonoBehaviour
{
    public float jumpForce = 10f;
    public bool Jump;
    [SerializeField] string inputName;

    public movimientoPlayer movimiento;

    [Header("Animacion")]
    public bool isJumping;
    [SerializeField] string animationName;

    public float fallMultiplier = 8f;

    public bool canJump;

    private void Update()
    {
        // Captura de la entrada de salto
        if (Input.GetButtonDown(inputName) && canJump && movimiento.isGrounded)
        {
            canJump = false;
            Jump = true;
        }
    }

    private void FixedUpdate()
    {
        // Aplicar el salto si Jump es true
        if (Jump)
        {
            jump();
            Jump = false; // Reinicia el salto después de aplicarlo
        }

        // Aplica la gravedad adicional cuando el jugador comienza a descender
        if (isJumping && movimiento.rb.linearVelocity.y < 0)
        {
            ApplyAdditionalGravity();
        }
    }

    public void jump()
    {
        movimiento.irisAnimation.Play(animationName);
        movimiento.rb.linearVelocity += (Vector3.up * jumpForce);
        isJumping = true;
        movimiento.isGrounded = false;
    }

    private void ApplyAdditionalGravity()
    {
        movimiento.rb.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1), ForceMode.Acceleration);
    }
}
