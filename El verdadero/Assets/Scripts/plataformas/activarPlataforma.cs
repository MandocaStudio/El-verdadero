using UnityEngine;

public class activarPlataforma : MonoBehaviour
{

    public Elevators elevators;

    public movimientoPlayer playerMovement;

    [SerializeField] private bool inPlatform;
    void Update()
    {
        if (inPlatform == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                elevators.moving = true;
            }


            if (elevators.moving == true)
            {
                playerMovement.canMove = false;
            }
            else
            {
                playerMovement.canMove = true;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPlatform = true;
            playerMovement = other.GetComponent<movimientoPlayer>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPlatform = false;
            playerMovement = null;
        }
    }
}
