using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequenceManager : MonoBehaviour
{
    public DialogueSequenceSO dialogueSequence;  // Referencia por defecto a la secuencia de diálogos
    public DialogueManagerSO dialogueManager;    // Referencia al administrador de diálogos

    private int currentDialogueIndex = 0;  // Índice del diálogo actual

    void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = Object.FindAnyObjectByType<DialogueManagerSO>();  // Buscar automáticamente el DialogueManager
        }

        if (dialogueManager != null)
        {
            dialogueManager.sequenceManager = this;  // Asignar el secuenciador al DialogueManagerSO
        }
        else
        {
            Debug.LogError("No se encontró DialogueManagerSO en la escena.");
        }
    }

    // Inicia la secuencia de diálogos con los elementos de DialogueSequenceSO
    public void StartDialogueSequence(DialogueSequenceSO newDialogueSequence)
    {
        dialogueSequence = newDialogueSequence;  // Actualizar la secuencia de diálogos con la nueva
        if (dialogueSequence != null && dialogueSequence.dialogueElements.Length > 0)
        {
            dialogueManager.StartDialogue(dialogueSequence);  // Inicia la secuencia de diálogos usando el nuevo formato de DialogueSequenceSO
        }
        else
        {
            Debug.LogWarning("La secuencia de diálogos está vacía o es nula.");
        }
    }

    // Llama a este método para avanzar al siguiente diálogo en la secuencia
    public void NextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogueSequence.dialogueElements.Length)
        {
            dialogueManager.DisplayNextSentence();  // Muestra el siguiente diálogo en la secuencia
        }
        else
        {
            Debug.Log("Secuencia de diálogos completada.");
        }
    }

    // Función pública para activar la secuencia de diálogos desde otro script, pasando un nuevo DialogueSequenceSO
    public void ActivateDialogueSequence(DialogueSequenceSO newDialogueSequence)
    {
        StartDialogueSequence(newDialogueSequence);  // Inicia la secuencia de diálogos con la nueva secuencia pasada como parámetro
    }
}
