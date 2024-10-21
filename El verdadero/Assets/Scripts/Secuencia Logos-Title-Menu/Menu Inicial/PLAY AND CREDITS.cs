using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU : MonoBehaviour
{
    public AudioSource soundtrack; // Asegúrate de asignar tu AudioSource en el inspector
    public GameObject menuCanvas;  // Agrega aquí el Canvas del menú inicial
    private string menuSceneName = "MENU INICIAL"; // Usa el nombre exacto de tu escena de menú inicial

    void Start()
    {
        // Asegurar que el Canvas del menú esté activo al inicio
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(true);
        }

        // Suscribirse al evento de carga de escenas
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void play()
    {
        // Desactiva el Canvas del menú inicial al comenzar el juego
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(false);
        }

        // Cargar la escena "Game" directamente
        SceneManager.LoadScene("Introduccion");
    }

    public void quit()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Sale del juego
    }

    // Método llamado cuando se carga cualquier escena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena cargada es la del menú inicial utilizando el nombre
        if (scene.name == menuSceneName && menuCanvas != null)
        {
            menuCanvas.SetActive(true); // Reactivar el Canvas del menú inicial
        }

        // Desuscribirse del evento para evitar que se ejecute de nuevo
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Desuscribirse del evento al destruir el objeto para evitar errores
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
