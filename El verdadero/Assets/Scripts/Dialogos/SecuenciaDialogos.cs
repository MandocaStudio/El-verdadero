using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Dialogue/Dialogue Sequence")]
public class DialogueSequenceSO : ScriptableObject
{
    public DialogueElementSO[] dialogueElements;  // Arreglo de elementos de diálogo (texto y audio opcional)
}
