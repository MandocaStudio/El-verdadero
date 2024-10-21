using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [Header("Rutina")]
    public Animator animator;

    public Rigidbody enemyRB;
    public int rutina;
    public float cronometro;
    public quaternion angle;
    public float grado;

    public bool isAttacking;
    public bool stuneado;
    public float RadioVision;
    public float speed;
    public float distanciaAtaque;
    public NavMeshAgent Agente;

    [Header("Stats")]
    public float health = 100;

    [Header("Retroceso al recibir daño")]
    public float retreatDistance = 2f; // Distancia a retroceder al recibir daño
    public float retreatDuration = 0.2f; // Duración del retroceso

    private bool isRetreating = false;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(RetreatAfterDamage());
        }
    }

    private void Die()
    {
        Debug.Log("Muerto");
        Destroy(gameObject);
    }

    private IEnumerator RetreatAfterDamage()
    {
        if (isRetreating) yield break; // Si ya está retrocediendo, no hacer nada

        enemyRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        enemyRB.constraints = RigidbodyConstraints.FreezeRotation;
        isRetreating = true;

        // Desactivar temporalmente el NavMeshAgent para control manual
        Agente.enabled = false;

        // Calcular la dirección contraria al jugador
        Vector3 retreatDirection = (transform.position - GameObject.FindWithTag("Player").transform.position).normalized;

        // Mantener la posición en Y y calcular la nueva posición en XZ
        Vector3 retreatTarget = new Vector3(
            transform.position.x + retreatDirection.x * retreatDistance,
            transform.position.y, // Mantener la posición en Y
            transform.position.z + retreatDirection.z * retreatDistance
        );

        float elapsedTime = 0f;

        // Moverse hacia el objetivo de retroceso durante un tiempo específico
        while (elapsedTime < retreatDuration)
        {
            // Mantener la posición en Y durante la interpolación
            transform.position = Vector3.Lerp(transform.position, retreatTarget, (elapsedTime / retreatDuration));
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); // Mantener la posición en Y

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        enemyRB.constraints &= ~RigidbodyConstraints.FreezePositionY;
        // Reactivar el NavMeshAgent después del retroceso
        Agente.enabled = true;

        isRetreating = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sword"))
        {

        }
    }
}
