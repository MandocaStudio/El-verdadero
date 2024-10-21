using System.Collections;
using UnityEngine;

public class spawnAfterTransitionAnimation : MonoBehaviour
{
    public bool canUse;

    public bool isPlayerInTrigger;

    [Header("Colliders")]

    public Transform playerTransform;
    public Transform spawn;

    private void Update()
    {
        if (isPlayerInTrigger == true && Input.GetButton("Interact"))
        {
            StartCoroutine(spawnChanger());
        }
    }

    IEnumerator spawnChanger()
    {

        yield return new WaitForSeconds(1);
        playerTransform.position = spawn.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            playerTransform = other.transform;

            isPlayerInTrigger = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = false;

            playerTransform = null;
        }
    }
}
