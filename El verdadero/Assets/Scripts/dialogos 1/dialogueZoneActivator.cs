using UnityEngine;

public class dialogueZoneActivator : MonoBehaviour
{

    public DialogueSequenceManager managerSecuencia;

    public DialogueSequenceSO mensaje;

    public GameObject managerObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

        }
    }


}
