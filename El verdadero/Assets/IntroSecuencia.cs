using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecuencialIntroduccion : MonoBehaviour
{
    [System.Serializable]
    public class PanelInfo
    {
        public GameObject panel;            // Referencia al Panel completo
        public CanvasGroup mainImageGroup;  // CanvasGroup de la imagen principal para aplicar fade
        public CanvasGroup textGroup;       // CanvasGroup del texto para aplicar fade
        public float mainDuration = 4f;     // Duración para mostrar la imagen principal y el texto
    }

    public Image backgroundImage;           // Imagen de fondo general que se mostrará al inicio
    public PanelInfo[] panels;              // Lista de paneles con su duración
    public float fadeDuration = 1f;         // Duración del fade-in y fade-out de los paneles
    public float finalFadeDuration = 1.5f;  // Duración del fundido a negro y aclarado al final
    public GameObject parentCanvas;         // Canvas padre que se desactivará al final (Introduction)
    public AudioSource audioSource;         // Componente de AudioSource para la música de fondo
    public AudioClip introMusic;            // Clip de audio que se reproducirá durante la secuencia
    public CanvasGroup fadeOutPanelGroup;   // CanvasGroup del panel de fundido a negro

    private bool skipPanel = false;         // Bandera para detectar si el usuario quiere saltar el panel actual

    private void Start()
    {
        StartCoroutine(ShowPanelsSequence());
    }

    private void Update()
    {
        // Detectar entradas para saltar (Espacio, Click o Botón del mando específico)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            skipPanel = true; // Cambia la bandera para saltar el panel
        }
    }

    private IEnumerator ShowPanelsSequence()
    {
        // Asegurarse de que la imagen de fondo está visible desde el principio y permanece visible
        backgroundImage.gameObject.SetActive(true);

        // Desactivar todos los paneles inicialmente y asegurar que las imágenes y textos están ocultos
        foreach (var panelInfo in panels)
        {
            panelInfo.panel.SetActive(false);                // Desactivar el panel completo
            panelInfo.mainImageGroup.alpha = 0f;             // Asegurar que la imagen principal está invisible
            panelInfo.textGroup.alpha = 0f;                  // Asegurar que el texto está invisible
        }

        // Reproducir la música de introducción y sincronizar con la duración exacta
        if (audioSource != null && introMusic != null)
        {
            audioSource.clip = introMusic;
            audioSource.Play();
        }

        // Mostrar cada panel con su duración basado en el tiempo del AudioSource
        foreach (var panelInfo in panels)
        {
            skipPanel = false; // Reiniciar la bandera de skip para el nuevo panel
            panelInfo.panel.SetActive(true); // Activar el panel completo

            // Fade-in de la imagen principal
            yield return StartCoroutine(FadeIn(panelInfo.mainImageGroup, fadeDuration));

            // Fade-in del texto después
            yield return StartCoroutine(FadeIn(panelInfo.textGroup, fadeDuration));

            // Esperar el tiempo de la duración exacta basado en AudioSource, o hasta que se detecte un skip
            float startTime = Time.time;
            while (Time.time < startTime + panelInfo.mainDuration)
            {
                if (skipPanel) break; // Si se detecta un skip, salir del bucle
                yield return null;
            }

            // Hacer Fade-out del texto
            yield return StartCoroutine(FadeOut(panelInfo.textGroup, fadeDuration));

            // Hacer Fade-out de la imagen principal
            yield return StartCoroutine(FadeOut(panelInfo.mainImageGroup, fadeDuration));

            panelInfo.panel.SetActive(false); // Desactivar el panel completo
        }

        // Detener la música de fondo
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // Hacer el fade a negro y mantenerlo hasta que cargue la nueva escena
        yield return StartCoroutine(FinalFadeAndLoadScene("Game"));

        // Desactivar el canvas padre después de que el fade final se complete
        parentCanvas.SetActive(false);
    }

    // Funciones auxiliares de fade
    private IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / duration); // Efecto de fade-in
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, counter / duration); // Efecto de fade-out
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }

    // Función para hacer el fade a negro y cargar la escena
    private IEnumerator FinalFadeAndLoadScene(string sceneName)
    {
        fadeOutPanelGroup.gameObject.SetActive(true);
        float counter = 0f;

        // Fade-in a negro
        while (counter < finalFadeDuration)
        {
            counter += Time.deltaTime;
            fadeOutPanelGroup.alpha = Mathf.Lerp(0, 1, counter / finalFadeDuration);
            yield return null;
        }
        fadeOutPanelGroup.alpha = 1f; // Asegurar que esté completamente opaco

        // Desactivar la imagen de fondo cuando el fade está completo
        backgroundImage.gameObject.SetActive(false);

        // Cargar la nueva escena de forma asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Esperar hasta que la escena esté completamente cargada
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
