using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class apears : MonoBehaviour
{

    public GameObject enemies;

    public CinemachineCamera camara;

    public AudioClip combatMusic;

    [SerializeField] private bool corrutingRuning = false;
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

            player.irisAnimation.Stop("IdleBrush");


            player.canMove = false;


            // Iniciar la coroutine
            StartCoroutine(spidersitas(player));

            // Si no está en una corrida corrupta, habilitar el movimiento del jugador

        }
    }




    IEnumerator spidersitas(movimientoPlayer player)
    {

        camara.Priority = 2;


        yield return new WaitForSeconds(3);

        enemies.SetActive(true);

        player.soundtrack.clip = combatMusic;

        player.soundtrack.Play();

        player.irisAnimation.Stop("IdleBrush");



        yield return new WaitForSeconds(2);
        camara.Priority = 0;

        player.canMove = true;
        Destroy(gameObject);


    }
}
