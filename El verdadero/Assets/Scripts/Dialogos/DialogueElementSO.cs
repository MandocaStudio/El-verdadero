using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Element", menuName = "Dialogue/Dialogue Element")]
public class DialogueElementSO : ScriptableObject
{
    public string characterName;  // Nombre del personaje que habla esta línea
    [TextArea(3, 10)] public string sentence;  // Texto de la línea de diálogo
    public AudioClip audioClip;   // Clip de audio opcional para esta línea
}