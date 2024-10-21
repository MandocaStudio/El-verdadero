using UnityEngine;
using UnityEngine.SceneManagement;  // Para gestionar las escenas.
using UnityEngine.UI;               // Para controlar los botones.
using UnityEngine.EventSystems;     // Para manejar la selección automática del menú.

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;          // Referencia al panel de pausa.
    public Button resumeButton;            // Referencia al botón "Volver al Juego".
    public Button restartButton;           // Referencia al botón "Reiniciar".
    public Button exitButton;              // Referencia al botón "Salir al Menú Inicial".
    private bool isPaused = false;         // Indica si el juego está en pausa.

    public movimientoPlayer player;

    public int scene;


    void Start()
    {
        // Asegurar que el menú de pausa está desactivado al inicio.
        if (pausePanel == null)
        {
            Debug.LogError("El Pause Panel no está asignado en el Inspector.");
        }

        pausePanel.SetActive(false);

        // Asignar las funciones a los botones.
        if (resumeButton != null) resumeButton.onClick.AddListener(ResumeGame);
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (exitButton != null) exitButton.onClick.AddListener(ExitToMainMenu);
    }

    void Update()
    {
        // Imprimir mensaje para verificar si la tecla Esc se detecta.
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))

        {
            player.enabled = false;
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();  // Pausar el juego.
            }
        }
    }

    // Método para pausar el juego y mostrar el menú de pausa.
    public void PauseGame()
    {
        Debug.Log("Juego pausado. Mostrando el menú de pausa.");
        pausePanel.SetActive(true);             // Mostrar el panel de pausa.
        Time.timeScale = 0f;                    // Detener el tiempo en el juego.
        isPaused = true;                        // Marcar que el juego está en pausa.
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);  // Seleccionar el botón "Volver al Juego".
    }

    // Método para reanudar el juego desde el punto de pausa.
    public void ResumeGame()
    {
        Debug.Log("Juego reanudado.");
        pausePanel.SetActive(false);            // Ocultar el panel de pausa.
        Time.timeScale = 1f;                    // Reanudar el tiempo normal.
        isPaused = false;
        player.enabled = true;

    }

    // Método para reiniciar la escena actual.
    public void RestartGame()
    {
        Debug.Log("Reiniciando la escena actual.");
        Time.timeScale = 1f;  // Asegurar que el tiempo está en modo normal.
        SceneManager.LoadScene(scene);
    }

    // Método para salir al menú inicial.
    public void ExitToMainMenu()
    {
        Debug.Log("Saliendo al menú inicial.");
        Time.timeScale = 1f;  // Asegurar que el tiempo está en modo normal.
        SceneManager.sceneLoaded += OnSceneLoaded;  // Suscribirse al evento de carga de la escena.
        SceneManager.LoadScene(2);  // Cargar la escena 2, que es el menú inicial.
    }

    // Método para reactivar el Canvas al cargar la escena del menú inicial.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)  // Si la escena cargada es la del menú inicial.
        {
            GameObject canvasMenu = GameObject.Find("menu");  // Busca el objeto "menu" en la nueva escena.
            if (canvasMenu != null)
            {
                canvasMenu.SetActive(true);  // Reactiva el Canvas del menú inicial.
            }

            // Desuscribirse del evento para evitar que se ejecute de nuevo.
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}