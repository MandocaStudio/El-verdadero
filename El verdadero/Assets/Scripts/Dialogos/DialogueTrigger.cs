using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueElementSO dialogueElement;  // Asigna el ScriptableObject DialogueElementSO desde el Inspector

    void Start()
    {
        StartCoroutine(InitializeDialogue());
    }

    IEnumerator InitializeDialogue()
    {
        yield return new WaitForSeconds(0.1f);  // Espera 0.1 segundos para asegurar que DialogueManager está listo

        DialogueManagerSO dialogueManager = Object.FindAnyObjectByType<DialogueManagerSO>();

        if (dialogueManager == null)
        {
            Debug.LogError("No se encontró el DialogueManagerSO en la escena. Por favor, revisa las referencias.");
            yield break;  // Detener la ejecución si no se encuentra el manager
        }

        if (dialogueElement != null)
        {
            // Crear un DialogueSequenceSO temporalmente para almacenar un solo DialogueElementSO
            DialogueSequenceSO tempSequence = ScriptableObject.CreateInstance<DialogueSequenceSO>();
            tempSequence.dialogueElements = new DialogueElementSO[] { dialogueElement };  // Asignar el único elemento a la secuencia

            dialogueManager.StartDialogue(tempSequence);  // Inicia la secuencia con un solo elemento
        }
        else
        {
            Debug.LogError("No se encontró el DialogueElementSO. Por favor, asigna el ScriptableObject de diálogo en el Inspector.");
        }
    }
}
