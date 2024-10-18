using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasButtonSoundController : MonoBehaviour
{
    public AudioClip hoverSound;   // Sonido que se reproduce al pasar el puntero (hover).
    public AudioClip clickSound;   // Sonido que se reproduce al hacer clic en el botón.
    public AudioClip selectSound;  // Sonido que se reproduce al seleccionar el botón con el teclado/controlador.
    private AudioSource audioSource;

    void Start()
    {
        // Buscar un AudioSource en el Canvas o en el mismo objeto donde está asignado este script.
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // Si no se encuentra un AudioSource, añadir uno al Canvas.
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Asegurarse de que no reproduzca automáticamente.
        audioSource.playOnAwake = false;

        // Encontrar y recorrer todos los botones dentro de este Canvas.
        Button[] buttons = GetComponentsInChildren<Button>();

        // Asignar los eventos a cada botón encontrado.
        foreach (Button button in buttons)
        {
            // Asignar eventos de sonido a cada botón.
            AddEventTriggers(button);
        }
    }

    // Método para asignar los eventos de sonido a cada botón.
    private void AddEventTriggers(Button button)
    {
        // Asignar sonido de clic.
        button.onClick.AddListener(() => PlayClickSound());

        // Añadir un EventTrigger para detectar `PointerEnter` y reproducir el sonido de `hover`.
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        // Crear el evento de `PointerEnter` para detectar cuando el ratón pasa por encima.
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        hoverEntry.callback.AddListener((eventData) => { PlayHoverSound(); });
        trigger.triggers.Add(hoverEntry);

        // Crear el evento de `Select` para detectar cuando el botón es seleccionado con teclado/control.
        EventTrigger.Entry selectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Select
        };
        selectEntry.callback.AddListener((eventData) => { PlaySelectSound(); });
        trigger.triggers.Add(selectEntry);
    }

    // Método para reproducir el sonido de clic.
    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    // Método para reproducir el sonido de hover.
    public void PlayHoverSound()
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // Método para reproducir el sonido de selección.
    public void PlaySelectSound()
    {
        if (selectSound != null)
        {
            audioSource.PlayOneShot(selectSound);
        }
    }
}
