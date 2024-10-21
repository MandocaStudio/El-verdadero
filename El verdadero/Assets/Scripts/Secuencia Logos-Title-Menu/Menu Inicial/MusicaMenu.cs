using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource menuAudioSource;  // El componente AudioSource que controlará la música del menú
    public AudioClip menuSoundtrack;     // El clip de audio que se reproducirá en el menú
    public GameObject bandaSonoraCanvas; // El submenú de Banda Sonora dentro del Canvas principal

    private bool isMenuMusicPlaying = false;  // Controla si la música del menú se está reproduciendo
    private string sceneMenu = "MENU INICIAL";   // Nombre exacto de la escena de menú inicial
    [SerializeField] string sceneGame;  // Nombre exacto de la escena del juego

    void Start()
    {
        // Asignar el AudioClip al AudioSource
        if (menuAudioSource != null && menuSoundtrack != null)
        {
            menuAudioSource.clip = menuSoundtrack;
        }

        // Comenzar la reproducción de la música en el menú inicial
        PlayMenuMusic();

        // Suscribirse al evento de carga de escenas para saber cuándo volver al menú
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Método para comenzar la música del menú
    public void PlayMenuMusic()
    {
        if (menuAudioSource != null)
        {
            menuAudioSource.Stop();  // Asegurarse de que la música no esté sonando
            menuAudioSource.Play();  // Iniciar la reproducción
            isMenuMusicPlaying = true;
        }
    }

    // Método para detener la música del menú
    public void StopMenuMusic()
    {
        if (menuAudioSource != null && menuAudioSource.isPlaying)
        {
            menuAudioSource.Stop();
            isMenuMusicPlaying = false;
        }
    }

    // Método para pausar la música del menú (al entrar en Banda Sonora)
    public void PauseMenuMusic()
    {
        if (menuAudioSource != null && menuAudioSource.isPlaying)
        {
            menuAudioSource.Pause();
        }
    }

    // Método para reanudar la música del menú (al salir de Banda Sonora)
    public void ResumeMenuMusic()
    {
        if (menuAudioSource != null && !menuAudioSource.isPlaying && isMenuMusicPlaying)
        {
            menuAudioSource.UnPause();
        }
    }

    // Controlador del evento al cargar la escena para verificar si se ha vuelto al menú
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si estamos en la escena del menú inicial, reiniciar la música desde cero
        if (scene.name == sceneMenu)
        {
            PlayMenuMusic();  // Reproducir desde cero al entrar en el menú
        }
        else if (scene.name == sceneGame)
        {
            StopMenuMusic();  // Detener la música del menú al comenzar el juego
        }
    }

    // Método para entrar en el submenú de Banda Sonora
    public void EnterBandaSonora()
    {
        if (bandaSonoraCanvas != null)
        {
            PauseMenuMusic();  // Pausar la música del menú
            bandaSonoraCanvas.SetActive(true);  // Activar el submenú de Banda Sonora
        }
    }

    // Método para salir del submenú de Banda Sonora y volver al menú inicial
    public void ExitBandaSonora()
    {
        if (bandaSonoraCanvas != null)
        {
            bandaSonoraCanvas.SetActive(false);  // Desactivar el submenú de Banda Sonora
            ResumeMenuMusic();  // Reanudar la música del menú
        }
    }

    // Método para gestionar el volumen desde un Slider (opcional)
    public void SetVolume(float volume)
    {
        if (menuAudioSource != null)
        {
            menuAudioSource.volume = volume;  // Ajustar el volumen según el valor del Slider
        }
    }

    // Desuscribirse al destruir el objeto
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
