using UnityEngine;

public class musica : MonoBehaviour
{

    public AudioSource soundtrack;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        // Detener la reproducción del AudioSource al cambiar de escena
        if (soundtrack.isPlaying)
        {
            soundtrack.Stop();
        }
    }
}
