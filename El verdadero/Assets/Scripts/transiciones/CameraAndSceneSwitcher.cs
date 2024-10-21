using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Cinemachine;
using System.Collections;

public class CameraAndSceneSwitcher : MonoBehaviour
{
    public CinemachineCamera cameraPuzle;
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f; // Duración del fundido a negro
    public string scenaString;
    public int scenaInt = -1; // Valor predeterminado (-1 para indicar que no está configurado)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cambiar la prioridad de la cámara
            cameraPuzle.Priority = 2;

            // Iniciar la secuencia de cambio de escena con un retraso
            StartCoroutine(SwitchSceneWithDelay());
        }
    }

    IEnumerator SwitchSceneWithDelay()
    {
        // Esperar 5 segundos con la nueva cámara activa
        yield return new WaitForSeconds(5f);

        // Iniciar el fundido a negro
        yield return StartCoroutine(FadeToBlack());

        // Cambiar de escena
        if (!string.IsNullOrEmpty(scenaString))
        {
            SwitchToSceneByName();
        }
        else if (scenaInt >= 0)
        {
            SwitchToSceneByIndex();
        }
    }

    // Realiza el fundido a negro
    IEnumerator FadeToBlack()
    {
        float timer = 0f;
        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, timer / fadeDuration); // Ajustar opacidad del Canvas
            yield return null;
        }
    }

    // Cambiar a una escena por su nombre
    public void SwitchToSceneByName()
    {
        SceneManager.LoadScene(scenaString);
    }

    // Cambiar a una escena por su índice
    public void SwitchToSceneByIndex()
    {
        SceneManager.LoadScene(scenaInt);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Restaurar la prioridad de la cámara cuando el jugador salga del trigger
            cameraPuzle.Priority = 0;
        }
    }
}
