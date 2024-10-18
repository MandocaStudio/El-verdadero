using UnityEngine;

public class AudioListenerManager : MonoBehaviour
{
    void Start()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();  // Buscar todos los Audio Listeners en la escena.

        // Si hay más de un Audio Listener, desactivar los adicionales.
        for (int i = 1; i < listeners.Length; i++)
        {
            listeners[i].enabled = false;  // Desactivar el Audio Listener adicional.
        }
    }
}
