using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActivator : MonoBehaviour
{
    public GameObject menuCanvas; // Asigna el objeto "menu" desde el inspector
    private string menuSceneName = "MENU INICIAL"; // Nombre de la escena del menú inicial
    private int menuSceneIndex = 2;  // Índice de la escena del menú inicial

    void Start()
    {
        // Asegurarse de que el menú esté activado cuando se carga la escena por primera vez
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(true);
        }

        // Suscribirse al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Método que se llama cada vez que se carga una escena nueva
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificar si la escena cargada es la escena "MENU INICIAL" o el índice 2
        if ((scene.name == menuSceneName || scene.buildIndex == menuSceneIndex) && menuCanvas != null)
        {
            menuCanvas.SetActive(true);  // Activa el Canvas del menú inicial
        }
    }

    // Desuscribirse del evento cuando el objeto se destruye para evitar errores
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
