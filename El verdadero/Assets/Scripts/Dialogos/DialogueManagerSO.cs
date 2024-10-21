using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManagerSO : MonoBehaviour
{
    public TMP_Text characterNameText;   // Referencia al componente TextMeshPro para el nombre del personaje
    public TMP_Text dialogueText;        // Referencia al componente TextMeshPro para el diálogo
    public Canvas dialogueCanvas;        // Referencia al Canvas que contiene el diálogo
    public float typingSpeed = 0.05f;    // Velocidad de escritura en segundos por letra
    public DialogueSequenceManager sequenceManager;  // Referencia al secuenciador de la secuencia
                                                     //   public AudioSource audioSource;      // AudioSource para reproducir los clips de audio

    private Queue<DialogueElementSO> dialogueElements;  // Cola para almacenar los elementos del diálogo como ScriptableObject
    private bool textFullyDisplayed = false;  // Indica si todo el texto ya ha sido mostrado

    void Awake()
    {
        dialogueElements = new Queue<DialogueElementSO>();  // Inicializa la cola para los elementos de diálogo
        dialogueCanvas.enabled = false;  // Oculta el Canvas de diálogo al inicio
    }

    void Update()
    {
        // Si el texto está completamente desplegado, permitir avanzar con Espacio o clic del ratón
        if (textFullyDisplayed && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            DisplayNextSentence();  // Avanza al siguiente diálogo
        }
    }

    // Iniciar el diálogo usando ScriptableObjects con elementos de diálogo
    public void StartDialogue(DialogueSequenceSO dialogueSequence)
    {
        dialogueCanvas.enabled = true;  // Muestra el Canvas completo

        dialogueElements.Clear();  // Limpia la cola de elementos de diálogo
        foreach (DialogueElementSO element in dialogueSequence.dialogueElements)
        {
            dialogueElements.Enqueue(element);  // Agrega cada elemento de diálogo a la cola
        }

        DisplayNextSentence();  // Muestra la primera línea de diálogo con su audio si está disponible
    }

    // Muestra la siguiente oración en la cola y reproduce el audio correspondiente
    public void DisplayNextSentence()
    {
        if (dialogueElements.Count == 0)
        {
            EndDialogue();  // Si no hay más oraciones, termina el diálogo y avisa a la secuencia
            return;
        }

        DialogueElementSO currentElement = dialogueElements.Dequeue();  // Toma el siguiente elemento de diálogo de la cola

        StopAllCoroutines();  // Detiene cualquier animación de texto anterior
        StartCoroutine(TypeSentence(currentElement));  // Inicia la animación de escritura con la velocidad ajustada

        // Reproducir el audio correspondiente a la línea de diálogo si existe
        // if (currentElement.audioClip != null && audioSource != null)
        // {
        //     audioSource.Stop();
        //     audioSource.clip = currentElement.audioClip;
        //     audioSource.Play();
        // }
    }

    // Simula el efecto de escritura en pantalla con velocidad ajustable
    IEnumerator TypeSentence(DialogueElementSO element)
    {
        textFullyDisplayed = false;  // Aún no se ha mostrado completamente
        characterNameText.text = element.characterName;  // Establece el nombre del personaje
        dialogueText.text = "";  // Limpia el campo de texto

        foreach (char letter in element.sentence.ToCharArray())
        {
            dialogueText.text += letter;  // Agrega letra por letra
            yield return new WaitForSeconds(typingSpeed);  // Espera un tiempo específico antes de agregar la siguiente letra
        }

        // Indica que el texto ya ha sido mostrado completamente
        textFullyDisplayed = true;
    }

    // Termina el diálogo actual y avisa al DialogueSequenceManager
    public void EndDialogue()
    {
        if (sequenceManager != null)
        {
            sequenceManager.NextDialogue();  // Notifica al secuenciador para mostrar el siguiente diálogo
        }
        else
        {
            Debug.LogError("El secuenciador no está asignado en DialogueManager.");
        }
    }
}
