using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class apearsExit : MonoBehaviour
{


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movimientoPlayer player = other.GetComponent<movimientoPlayer>();

            player.soundtrack.clip = player.chil;



            // Si no está en una corrida corrupta, habilitar el movimiento del jugador

        }
    }




}
