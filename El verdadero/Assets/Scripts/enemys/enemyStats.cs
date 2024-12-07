using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100;

    public EnemyMovement enemy;

    public void TakeDamage(float damage)
    {
        health -= damage;



        enemy.animator.SetBool("attack", false);
        enemy.animator.SetBool("walk", false);

        //obtener distancia contraria al jugador
        Vector3 retreatDirection = (transform.position - enemy.player.position).normalized;
        Vector3 retreatTarget = transform.position + retreatDirection * 3;
        StartCoroutine(RetreatAfterTakeDamage(retreatTarget));

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator RetreatAfterTakeDamage(Vector3 retreatTarget)
    {
        Debug.Log("retroceder");


        // Desactivar el NavMeshAgent mientras se realiza el retroceso
        enemy.agent.enabled = false;

        float retreatDuration = 0.5f; // Duración del retroceso
        float elapsedTime = 0f;

        // Mover hacia el objetivo de retroceso
        while (elapsedTime < retreatDuration)
        {
            transform.position = Vector3.Lerp(transform.position, retreatTarget, elapsedTime / retreatDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reactivar el NavMeshAgent después del retroceso
        enemy.agent.enabled = true;
    }

    private void Die()
    {
        Debug.Log("Muerto");
        Destroy(gameObject);
    }


}

