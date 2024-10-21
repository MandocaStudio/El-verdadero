using System.Collections;
using UnityEngine;

public class detector : MonoBehaviour
{
    public Animator animator;
    public EnemyStats enemy;

    public AnimationClip attackAnimationClip;  // Referencia al clip de animación de ataque
    public Collider detectorCollider;  // Referencia al Collider del detector

    private bool canAttack = true;

    private void Start()
    {
        // Asegúrate de que el Collider del detector esté asignado
        if (detectorCollider == null)
        {
            detectorCollider = GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canAttack && !enemy.stuneado)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
            StartCoroutine(AttackAction());
        }
    }

    IEnumerator AttackAction()
    {
        // Desactiva el movimiento y el Collider antes de atacar
        enemy.Agente.enabled = false;
        enemy.isAttacking = true;
        enemy.stuneado = true; // Evitar que el enemigo ataque mientras está aturdido
        detectorCollider.enabled = false;  // Desactiva el Collider del detector

        canAttack = false;  // Bloquea ataques adicionales durante la animación y enfriamiento

        // Inicia la animación de ataque
        animator.SetBool("attack", true);

        // Obtener la duración del clip de animación
        float attackDuration = attackAnimationClip.length;

        // Esperar la duración de la animación
        yield return new WaitForSeconds(attackDuration);

        // Detener la animación de ataque
        animator.SetBool("attack", false);

        // Esperar un enfriamiento de 2 segundos antes de permitir otro ataque
        yield return new WaitForSeconds(1.5f);

        // Reactivar movimiento, el Collider, y permitir que el enemigo ataque nuevamente
        enemy.isAttacking = false;
        enemy.stuneado = false;
        enemy.Agente.enabled = true;
        detectorCollider.enabled = true;  // Reactiva el Collider del detector
        canAttack = true;  // Permitir que el enemigo ataque nuevamente
    }
}
