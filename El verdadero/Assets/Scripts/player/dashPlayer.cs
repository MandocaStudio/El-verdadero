using System.Collections;
using UnityEngine;

public class dashPlayer : MonoBehaviour
{
    [Header("Variables cooldown del dash")]
    public bool canDash = true;
    public float dashCooldown = 2f; // Cooldown en segundos
    public float dashCooldownTimer;

    public float dashTimer;
    public movimientoPlayer player;



    [Header("Variables para el dash")]
    public float dashForce;
    public float dashDuration = 0.25f;
    public bool isDashing;

    public GameObject dashVFX;

    [Header("Indicador de direccion/rotacion")]

    public GameObject directionIndicator;
    private void Update()
    {
        // Manejamos el cooldown del dash
        if (!canDash)
        {

            dashCooldownTimer += Time.deltaTime;
            if (dashCooldownTimer >= dashCooldown)
            {
                canDash = true;
                dashCooldownTimer = 0f;
                return;
            }
        }


    }

    public IEnumerator dashAction()
    {
        AnimationClip dashAnimationClip = player.irisAnimation.GetClip("Dash");
        float animationDuration = dashAnimationClip.length;
        player.irisAnimation.Play("Dash");
        isDashing = true;
        canDash = false;

        // Aplicar la fuerza en la dirección de la rotación del personaje
        Vector3 dashDirection = directionIndicator.transform.forward * (dashForce + Time.deltaTime);
        player.rb.linearVelocity = dashDirection;

        // Freezear el movimiento en el eje Y (solo afecta al movimiento en Y)
        player.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        yield return new WaitForSeconds(animationDuration);

        dashVFX.SetActive(false);

        isDashing = false;
        player.rb.linearVelocity = Vector3.zero; // Detenemos el movimiento al finalizar el dash

        // Desactivar las restricciones después del dash
        player.rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation; // Eliminar cualquier restricción si es necesario.
    }


}
