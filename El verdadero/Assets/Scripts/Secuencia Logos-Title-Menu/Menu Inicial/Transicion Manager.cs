using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public Animator panelAnimator;

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // Aclarar el panel para mostrar el logo
        panelAnimator.SetTrigger("Lighten");
        yield return new WaitForSeconds(panelAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Esperar un momento antes de oscurecer
        yield return new WaitForSeconds(1);

        // Oscurecer el panel para ocultar el logo y luego cambiar de escena
        panelAnimator.SetTrigger("Darken");
        yield return new WaitForSeconds(panelAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Cambiar a la siguiente escena
        LoadNextScene();
    }

    void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "SceneMandocaLogo")
        {
            SceneManager.LoadScene("SceneComicBoomLogo");
        }
        else if (currentSceneName == "SceneComicBoomLogo")
        {
            SceneManager.LoadScene("SceneTitleScreen");
        }
        else if (currentSceneName == "SceneTitleScreen")
        {
            // Si es la última escena, puedes decidir qué hacer, como volver al menú principal o salir del juego
        }
    }
}
