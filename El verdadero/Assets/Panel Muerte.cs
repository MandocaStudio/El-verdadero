using UnityEngine;
using System.Collections;

public class PanelMuerte : MonoBehaviour
{
    public CanvasGroup canvasGroup;        // Para oscurecer la pantalla y manejar la opacidad.
    public float fadeDuration = 2.0f;      // Duración del fade de pantalla.
    public float delay = 1.0f;             // Retraso antes de iniciar el fade.
    private bool accionIniciada = false;   // Para evitar que la acción se inicie más de una vez.

    public bool activator = false;

    private void Start()
    {
        // Asegurarse de que el CanvasGroup esté inactivo al inicio.
        canvasGroup.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Activa la pantalla de muerte solo una vez.
        if (activator && !accionIniciada)
        {
            accionIniciada = true;  // Marca la acción como iniciada.
            StartCoroutine(OscurecerPantalla());
        }
    }

    IEnumerator OscurecerPantalla()
    {
        // Activar el CanvasGroup.
        canvasGroup.gameObject.SetActive(true);

        // Esperar un pequeño retraso antes de empezar.
        yield return new WaitForSecondsRealtime(delay);

        // Oscurecer la pantalla con fade.
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Usa Time.unscaledDeltaTime para mantener el tiempo en la corrutina.
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;  // Asegúrate de que la pantalla esté completamente oscura.
    }
}
