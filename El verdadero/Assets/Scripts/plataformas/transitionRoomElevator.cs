using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class transitionRoomElevator : MonoBehaviour
{
    public Elevators elevatorReceptorScript;

    public Transform elevatorReceptor;

    public GameObject transitionObject;

    public movimientoPlayer playerMovement;

    public transitionToPLace transitionAnimation;
    public Transform playerTransform;
    public bool canTransmision = true;
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transitionRoomElevator transitionScript = transitionObject.GetComponent<transitionRoomElevator>();
            Collider transitionCollider = transitionObject.GetComponent<Collider>();

            if (transitionScript.canTransmision == false)
            {
                return;
            }
            playerMovement = other.GetComponent<movimientoPlayer>();

            playerTransform = other.GetComponent<Transform>();
            transitionCollider.enabled = false;

            transitionAnimation.iniciate = true;

            StartCoroutine(elevatorActivated());

            transitionScript.canTransmision = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collider transitionCollider = transitionObject.GetComponent<Collider>();
            playerMovement = null;
            playerTransform = null;

            canTransmision = true;
            transitionAnimation.iniciate = false;
            transitionCollider.enabled = true;
        }
    }

    IEnumerator elevatorActivated()
    {
        yield return new WaitForSeconds(1);

        playerTransform.position = elevatorReceptor.position;
        elevatorReceptorScript.moving = true;
    }
}
