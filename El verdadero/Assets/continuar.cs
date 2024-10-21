using UnityEngine;
using UnityEngine.SceneManagement;
public class continuar : MonoBehaviour
{


    [SerializeField] int scene;

    public void GoToScene3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);

    }

    public void GoToSceneMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(2);
    }

}
