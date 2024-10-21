using UnityEngine;

public class dialogueZoneActivatorandNeverAgain : MonoBehaviour
{

    public DialogueSequenceManager managerSecuencia;

    public DialogueSequenceSO mensaje;

    public GameObject managerObject;

    public bool neverAgain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && neverAgain == false)
        {

            managerObject = GameObject.Find("Dialogos");

            Transform childTransform = managerObject.transform.Find("Manager Secuencia");

            managerSecuencia = childTransform.GetComponent<DialogueSequenceManager>();


            managerObject.GetComponent<Canvas>().enabled = true;


            managerSecuencia.ActivateDialogueSequence(mensaje);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            managerObject.GetComponent<Canvas>().enabled = false;

            managerObject = null;

            managerSecuencia = null;

            neverAgain = true;

        }
    }


}
