using UnityEngine;

public class dialogoCuracion : MonoBehaviour
{

    public DialogueSequenceManager managerSecuencia;

    public DialogueSequenceSO mensaje;

    public GameObject managerObject;

    public bool neverAgain;

    [SerializeField] string gameObjectCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(gameObjectCollider) && neverAgain == false)
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
        if (other.CompareTag(gameObjectCollider))
        {
            managerObject.GetComponent<Canvas>().enabled = false;

            managerObject = null;

            managerSecuencia = null;

            neverAgain = true;

        }
    }


}
