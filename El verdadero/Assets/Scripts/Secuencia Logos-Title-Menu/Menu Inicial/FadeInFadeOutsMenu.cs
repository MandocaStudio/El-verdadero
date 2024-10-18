using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransitionManager : MonoBehaviour
{
    [System.Serializable]
    public class CanvasGroupData
    {
        public string menuName;            // Nombre del menú (opcional, para organizar mejor)
        public CanvasGroup canvasGroup;    // CanvasGroup del menú
        public float fadeDuration = 0.5f;  // Duración del fade in/out para este menú
    }

    public CanvasGroupData[] menus;  // Lista de todos los CanvasGroups en el menú principal

    // Método para hacer Fade In en el Canvas especificado
    public void FadeInMenu(int menuIndex)
    {
        if (menuIndex < menus.Length)
            StartCoroutine(FadeCanvasGroup(menus[menuIndex].canvasGroup, 0f, 1f, menus[menuIndex].fadeDuration));
    }

    // Método para hacer Fade Out en el Canvas especificado
    public void FadeOutMenu(int menuIndex)
    {
        if (menuIndex < menus.Length)
            StartCoroutine(FadeCanvasGroup(menus[menuIndex].canvasGroup, 1f, 0f, menus[menuIndex].fadeDuration));
    }

    // Corrutina que realiza el efecto de Fade entre dos valores de Alpha
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        // Activar el Canvas si estamos haciendo Fade In
        if (endAlpha > startAlpha)
            cg.gameObject.SetActive(true);

        cg.alpha = startAlpha;

        while (elapsedTime < duration)
        {
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cg.alpha = endAlpha;

        // Desactivar el Canvas si estamos haciendo Fade Out
        if (endAlpha == 0f)
        {
            cg.interactable = false;
            cg.blocksRaycasts = false;
            cg.gameObject.SetActive(false);
        }
        else
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
    }
}
