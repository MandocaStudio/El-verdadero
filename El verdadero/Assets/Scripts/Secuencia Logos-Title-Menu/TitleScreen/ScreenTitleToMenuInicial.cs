using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    void Update()
    {
        // Detectar si cualquier tecla es presionada
        if (Input.anyKeyDown)
        {
            TransitionToMenu();
        }

        // Detectar si cualquier botón del joystick es presionado
        if (IsJoystickButtonPressed())
        {
            TransitionToMenu();
        }
    }

    void TransitionToMenu()
    {
        // Transicionar a la escena MENU INICIAL
        SceneManager.LoadScene("MENU INICIAL");
    }

    bool IsJoystickButtonPressed()
    {
        // Verificar todos los botones posibles del joystick
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                return true;
            }
        }
        return false;
    }
}