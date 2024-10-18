using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // Hacer que el objeto no se destruya entre escenas.
    }
}
