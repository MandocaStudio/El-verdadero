using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;  // Referencia al Canvas del menú principal.

    void Start()
    {
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);  // Activa el Canvas del menú inicial cuando se carga la escena.
        }
    }
}