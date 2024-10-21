using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    public EnemyStats enemy;
    public detector rango;
    public playerDetector playerDetector;

    public float rotationSpeed = 5f;
    public float distanceToRetreat = 5f; // Distancia prudente para retroceder

    private bool isRetreating = false;
    private float distanceToPlayer;

    private void Start()
    {
        // Inicializar referencias al jugador si ya está presente en la escena
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerDetector.playerTransform = player.GetComponent<Transform>();
            playerDetector.playerMovement = player.GetComponent<movimientoPlayer>();
        }
    }

    private void Update()
    {
        // Verifica si playerTransform y playerMovement no son nulos antes de ejecutar la lógica
        if (playerDetector.playerTransform != null && playerDetector.playerMovement != null)
        {



            if (!enemy.isAttacking)  // Solo ejecutar Comportamiento si no está atacando
            {
                Comportamiento();
            }
        }
        else if (playerDetector.playerTransform == null && playerDetector.playerMovement == null)
        {
            Patrullar();
        }
    }

    public void Comportamiento()
    {
        // Asegúrate de que playerTransform no sea nulo antes de calcular la distancia
        if (playerDetector.playerTransform != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerDetector.playerTransform.position);

            if (distanceToPlayer > enemy.RadioVision)
            {
                Patrullar();
            }
            else
            {
                Perseguir(distanceToPlayer);
            }
        }
        else
        {
            // Si no hay jugador detectado, patrullar
            Patrullar();
        }
    }

    private void Patrullar()
    {
        enemy.Agente.enabled = true;

        enemy.cronometro += Time.deltaTime;
        if (enemy.cronometro >= 4)
        {
            enemy.rutina = UnityEngine.Random.Range(0, 2);
            enemy.cronometro = 0;
        }

        switch (enemy.rutina)
        {
            case 0:
                enemy.animator.SetBool("walk", false);
                break;
            case 1:
                enemy.grado = UnityEngine.Random.Range(0, 360);
                enemy.angle = quaternion.Euler(0, enemy.grado, 0);
                enemy.rutina++;
                break;
            case 2:
                enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, enemy.angle, 0.5f);
                enemy.transform.Translate(Vector3.forward * enemy.speed * Time.deltaTime);
                enemy.animator.SetBool("walk", true);
                break;
        }
    }

    private void Perseguir(float distanceToPlayer)
    {
        enemy.cronometro = 4;
        if (playerDetector.playerTransform != null)
        {

            enemy.Agente.enabled = true;

            Vector3 lookPos = playerDetector.playerTransform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);

            enemy.Agente.SetDestination(playerDetector.playerTransform.position);

            if (distanceToPlayer > 1 && !enemy.isAttacking)
            {
                enemy.animator.SetBool("walk", true);
            }
            else if (!enemy.isAttacking)
            {
                enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                enemy.animator.SetBool("walk", false);
            }
        }
        else
        {
            Patrullar();  // Si no hay jugador detectado, el enemigo patrulla
        }
    }



    public void FinalAttack()
    {
        if (playerDetector.playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerDetector.playerTransform.position);

            if (distanceToPlayer > enemy.distanciaAtaque + 0.2f)
            {
                enemy.animator.SetBool("attack", false);
            }
        }

        enemy.isAttacking = false;
        enemy.stuneado = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;

        enemy.Agente.enabled = true;
        isRetreating = false; // Termina el estado de retroceso
    }
}
