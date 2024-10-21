using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] string scenaString;
    [SerializeField] int scenaInt = -1; // Establecer un valor predeterminado (-1 para indicar que no está configurado)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificar si se ha proporcionado el nombre de la escena
            if (!string.IsNullOrEmpty(scenaString))
            {
                SwitchToSceneByName();
            }
            // Verificar si se ha proporcionado el índice de la escena
            else if (scenaInt >= 0)
            {
                SwitchToSceneByIndex();
            }
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
}
