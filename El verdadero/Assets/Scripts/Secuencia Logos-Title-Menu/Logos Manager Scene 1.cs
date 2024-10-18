using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LogoTransitionController : MonoBehaviour
{
    public GameObject logo1; // Player 3 Academy
    public GameObject logo2; // Mandoca
    public GameObject logo3; // BoomJam
    public GameObject logo4; // ComicBoom
    public float displayTime = 3f; // Tiempo para mostrar los logos
    private bool showingFirstSet = true;
    private bool isTransitioning = false;

    void Start()
    {
        // Inicia la coroutine para mostrar los primeros logos
        StartCoroutine(DisplayFirstLogos());
    }

    void Update()
    {
        // Permitir que el jugador presione Espacio o el botón A para pasar a los siguientes logos
       if (Input.anyKeyDown && !isTransitioning)
        {
            if (showingFirstSet)
            {
                StopAllCoroutines();
                StartCoroutine(DisplaySecondLogos());
            }
            else
            {
                StopAllCoroutines();
                LoadNextScene();
            }
        }
    }

    IEnumerator DisplayFirstLogos()
    {
        logo1.SetActive(true);
        logo2.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        if (showingFirstSet)
        {
            StartCoroutine(DisplaySecondLogos());
        }
    }

    IEnumerator DisplaySecondLogos()
    {
        isTransitioning = true;
        showingFirstSet = false;
        logo1.SetActive(false);
        logo2.SetActive(false);
        logo3.SetActive(true);
        logo4.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        LoadNextScene();
    }

    void LoadNextScene()
    {
        isTransitioning = true;
        SceneManager.LoadScene("SceneTitleScreen"); // Asegúrate de que la escena "Title Screen" esté incluida en los Build Settings
    }
}
