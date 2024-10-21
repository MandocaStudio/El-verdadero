using System.Collections;
using UnityEngine;

public class dialogoGeneral : MonoBehaviour
{

    public DialogueSequenceManager managerSecuencia;

    public DialogueSequenceSO mensaje;

    public GameObject managerObject;

    [SerializeField] string inputName;

    [SerializeField] string axisName;



    [SerializeField] float time;



    [SerializeField] string gameObjectCollider;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //ubica el objeto que contiene el componente de dialogos en la jerarquia de objetos dentro de la escena

            managerObject = GameObject.Find("Dialogos");

            Transform childTransform = managerObject.transform.Find("Manager Secuencia");

            //

            managerSecuencia = childTransform.GetComponent<DialogueSequenceManager>();

            StartCoroutine(empezar());

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(finalizar());

        }
    }
    public IEnumerator empezar()
    {


        yield return new WaitForSeconds(time);
        managerSecuencia.ActivateDialogueSequence(mensaje);


        managerObject.GetComponent<Canvas>().enabled = true;

        yield return new WaitForSeconds(2);

    }


    public IEnumerator finalizar()
    {


        yield return new WaitForSeconds(0);

        managerObject.GetComponent<Canvas>().enabled = false;
        managerSecuencia.ActivateDialogueSequence(null);

    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(inputName) && Input.GetButtonDown(inputName) && managerObject != null)
        {
            managerObject.GetComponent<Canvas>().enabled = false;
            Destroy(this.gameObject);
        }

        if (!string.IsNullOrEmpty(axisName) && Input.GetAxis(axisName) > 0.5f && managerObject != null)
        {
            managerObject.GetComponent<Canvas>().enabled = false;
            Destroy(this.gameObject);
        }
    }

}
