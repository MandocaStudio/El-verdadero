using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float detectionRadius = 10f;
    public float attackDistance = 2f;
    public float retreatDistance = 5f;
    public float idleTimeAfterRetreat = 2f; // Tiempo estático tras retirarse

    [SerializeField] float Damage;
    public Transform player;
    public Animator animator;
    private Vector3 patrolTarget;
    [SerializeField] private bool isRetreating = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetNewPatrolTarget();
    }

    private void Update()
    {
        if (isRetreating) return; // Evitar ejecutar lógica si está en el proceso de retirada

        if (player == null) // Solo busca al jugador si no hay referencia guardada
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null && Vector3.Distance(transform.position, playerObject.transform.position) <= detectionRadius)
            {
                player = playerObject.transform; // Asigna al jugador solo si está en el rango
            }
        }
        else if (Vector3.Distance(transform.position, player.position) > detectionRadius)
        {
            player = null; // Limpia la referencia si el jugador está fuera del rango
        }

        if (player != null)
        {
            HandlePlayerInteraction();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            SetNewPatrolTarget();
            agent.SetDestination(patrolTarget);
            animator.SetBool("walk", true);
        }
    }

    private void SetNewPatrolTarget()
    {
        float randomX = transform.position.x + Random.Range(-10f, 10f);
        float randomZ = transform.position.z + Random.Range(-10f, 10f);
        patrolTarget = new Vector3(randomX, transform.position.y, randomZ);
    }

    private void HandlePlayerInteraction()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            // Calcular la dirección hacia el jugador
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Calcular el ángulo entre el frente del enemigo y la dirección hacia el jugador
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            // Solo atacar si el jugador está frente al enemigo (por ejemplo, dentro de un ángulo de 45 grados)
            if (angleToPlayer <= 10f)
            {
                agent.ResetPath(); // Detenerse para atacar
                animator.SetBool("attack", true);

                // Retirarse después de atacar
                Vector3 retreatDirection = (transform.position - player.position).normalized;
                Vector3 retreatTarget = transform.position + retreatDirection * retreatDistance;

                StartCoroutine(RetreatAfterAttack(retreatTarget));
            }
            else
            {
                animator.SetBool("attack", false);
            }
        }
        else
        {
            animator.SetBool("attack", false);
            agent.SetDestination(player.position);
            animator.SetBool("walk", true);
        }
    }




    private IEnumerator RetreatAfterAttack(Vector3 retreatTarget)
    {
        yield return new WaitForSeconds(0.5f);
        isRetreating = true;
        animator.SetBool("attack", false);

        // Caminar hacia la dirección contraria al jugador
        agent.SetDestination(retreatTarget);
        animator.SetBool("walk", true);

        // Esperar hasta que llegue al punto de retirada
        while (agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        // Permanecer estático por unos segundos
        animator.SetBool("walk", false);
        yield return new WaitForSeconds(idleTimeAfterRetreat);

        isRetreating = false; // Permitir volver a atacar o patrullar
    }

    // Método para dibujar el radio de detección en la vista de escena
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
    }
}
