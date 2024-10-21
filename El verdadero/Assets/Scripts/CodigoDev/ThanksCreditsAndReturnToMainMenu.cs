using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeElementsManager : MonoBehaviour
{
    public CanvasGroup[] elementosPanelGracias;
    public CanvasGroup[] elementosPanelCreditos;
    public CanvasGroup[] elementosPanelAgradecimientosEspeciales;
    public float fadeDuration = 1f;
    public float graciasDisplayTime = 5f;

    void Start()
    {
        SetInitialAlpha(elementosPanelGracias, 0f);
        SetInitialAlpha(elementosPanelCreditos, 0f);
        SetInitialAlpha(elementosPanelAgradecimientosEspeciales, 0f);
        StartCoroutine(ShowGraciasThenCredits());
    }

    IEnumerator ShowGraciasThenCredits()
    {
        yield return StartCoroutine(FadeInElements(elementosPanelGracias));
        yield return new WaitForSeconds(graciasDisplayTime);
        yield return StartCoroutine(FadeOutElements(elementosPanelGracias));
        yield return StartCoroutine(FadeInElements(elementosPanelCreditos));
        yield return StartCoroutine(FadeInElements(elementosPanelAgradecimientosEspeciales));
    }

    void SetInitialAlpha(CanvasGroup[] canvasGroups, float alphaValue)
    {
        foreach (CanvasGroup group in canvasGroups)
        {
            if (group != null)
                group.alpha = alphaValue;
        }
    }

    IEnumerator FadeInElements(CanvasGroup[] canvasGroups)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            foreach (CanvasGroup group in canvasGroups)
            {
                if (group != null)
                    group.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (CanvasGroup group in canvasGroups)
        {
            if (group != null)
                group.alpha = 1f;
        }
    }

    IEnumerator FadeOutElements(CanvasGroup[] canvasGroups)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            foreach (CanvasGroup group in canvasGroups)
            {
                if (group != null)
                    group.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (CanvasGroup group in canvasGroups)
        {
            if (group != null)
                group.alpha = 0f;
        }
    }

    public void OnReturnButtonPressed()
    {
        StartCoroutine(FadeOutElements(elementosPanelCreditos));
        StartCoroutine(FadeOutElements(elementosPanelAgradecimientosEspeciales));
        StartCoroutine(LoadMainMenuAfterFade());
    }

    IEnumerator LoadMainMenuAfterFade()
    {
        yield return new WaitForSeconds(fadeDuration);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MENU INICIAL");
    }
}
