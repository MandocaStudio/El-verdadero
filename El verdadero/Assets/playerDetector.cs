using Unity.VisualScripting;
using UnityEngine;

public class playerDetector : MonoBehaviour
{

    public Transform playerTransform;

    public movimientoPlayer playerMovement;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.GetComponent<Transform>();

            playerMovement = other.GetComponent<movimientoPlayer>();


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = null;

            playerMovement = null;


        }
    }
}
