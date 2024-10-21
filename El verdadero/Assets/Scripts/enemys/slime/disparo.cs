using UnityEngine;

public class disparo : MonoBehaviour
{

    public Transform player;



    public detectingPlayer playerDetected;

    void Update()
    {
        if (playerDetected.playerIsHere == true)
        {
            rotation();
        }
    }

    private void rotation()
    {
        player = GameObject.FindWithTag("Player").transform;


        // Calcula la dirección hacia el jugador
        Vector3 direction = player.position - transform.position;

        // Establece la rotación hacia el jugador
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Aplica la rotación al objeto (interpolación para una rotación suave)
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }
}

