using UnityEngine;
using TMPro;  // Importar para usar TextMeshPro.
using UnityEngine.UI;  // Importar para usar Slider.
using System.Collections.Generic;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> tracks;                 // Lista de pistas disponibles.
    public AudioSource audioSource;                // Componente AudioSource para reproducir la música.
    public TextMeshProUGUI currentTrackText;       // Texto de TextMeshPro para mostrar la pista actual.
    public Slider timeSlider;                      // Slider para mostrar y controlar el progreso de la canción.
    public TextMeshProUGUI timerText;              // Texto para mostrar el tiempo actual de la canción.
    private bool isDragging = false;               // Variable para saber si el usuario está arrastrando el Slider.
    private int currentTrackIndex = 0;             // Índice de la canción actual.
    private bool isPlaying = false;                // Controlar si la canción se está reproduciendo o no.

    void Start()
    {
        // Al iniciar, cargar la primera canción pero no reproducirla automáticamente.
        if (tracks.Count > 0)
        {
            audioSource.clip = tracks[currentTrackIndex];  // Cargar la primera pista en el AudioSource.
            UpdateTrackNameUI();  // Actualizar la UI con el nombre de la pista seleccionada.
        }
        ResetTimeSlider();  // Reiniciar el Slider y el contador.
    }

    void Update()
    {
        // Actualizar el valor del Slider con el tiempo actual de la canción y mostrar el tiempo en el contador, si no se está arrastrando.
        if (isPlaying && !isDragging)
        {
            UpdateTimeSlider();
            UpdateTimerText();
        }
    }

    // Método para iniciar la reproducción solo al presionar Play.
    public void PlayTrack()
    {
        if (audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.Play();  // Solo se reproducirá al dar clic en el botón de Play.
            isPlaying = true;
            UpdateTrackNameUI();
        }
    }

    // Método para detener la reproducción.
    public void StopTrack()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
            ResetTimeSlider();
        }
    }

    // Método para reproducir una pista específica por índice.
    public void PlayTrack(int index)
    {
        if (index < 0 || index >= tracks.Count) return;

        currentTrackIndex = index;
        audioSource.clip = tracks[currentTrackIndex];
        PlayTrack();  // Iniciar la reproducción.
        ResetTimeSlider();
    }

    // Método para pausar la reproducción.
    public void PauseTrack()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            isPlaying = false;
            if (currentTrackText != null)
            {
                currentTrackText.text = "Pausado:\n" + tracks[currentTrackIndex].name;
            }
        }
    }

    // Método para reproducir la siguiente pista.
    public void NextTrack()
    {
        if (tracks.Count == 0) return;  // Verificar que la lista no esté vacía.

        currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
        PlayTrack(currentTrackIndex);
    }

    // Método para reproducir la pista anterior.
    public void PreviousTrack()
    {
        if (tracks.Count == 0) return;  // Verificar que la lista no esté vacía.

        // Calcular el índice anterior de manera correcta para evitar valores negativos.
        currentTrackIndex = (currentTrackIndex - 1 + tracks.Count) % tracks.Count;
        PlayTrack(currentTrackIndex);
    }

    // Actualizar el nombre de la canción actual en la UI.
    void UpdateTrackNameUI()
    {
        if (currentTrackText != null)
        {
            currentTrackText.text = "Reproduciendo:\n" + tracks[currentTrackIndex].name;
        }
    }

    // Actualizar la barra de tiempo en función del progreso de la canción.
    void UpdateTimeSlider()
    {
        if (audioSource.clip != null)
        {
            timeSlider.value = audioSource.time / audioSource.clip.length;  // Normalizar el valor entre 0 y 1.
        }
    }

    // Actualizar el contador de tiempo en formato `Minuto:Segundo`.
    void UpdateTimerText()
    {
        if (audioSource.clip != null)
        {
            int minutes = Mathf.FloorToInt(audioSource.time / 60);  // Minutos transcurridos.
            int seconds = Mathf.FloorToInt(audioSource.time % 60);  // Segundos transcurridos.
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);  // Mostrar en formato `MM:SS`.
        }
    }

    // Listener para el Slider cuando se arrastra.
    void OnTimeSliderValueChanged(float value)
    {
        if (audioSource.clip != null && isDragging)
        {
            float newTime = value * audioSource.clip.length;
            if (newTime >= audioSource.clip.length)
            {
                newTime = audioSource.clip.length - 0.01f;
            }
            audioSource.time = newTime;
        }
    }

    // Método llamado cuando se empieza a arrastrar el Slider.
    public void OnPointerDown()
    {
        isDragging = true;
    }

    // Método llamado cuando se suelta el Slider después de arrastrar.
    public void OnPointerUp()
    {
        isDragging = false;
        float newTime = timeSlider.value * audioSource.clip.length;  // Calcula el nuevo tiempo de la canción.
        
        // Asegurar que el tiempo calculado no exceda la duración de la canción.
        if (newTime >= audioSource.clip.length)
        {
            newTime = audioSource.clip.length - 0.01f;  // Ajustar para evitar pasar el límite.
        }
        
        audioSource.time = newTime;  // Asigna el tiempo corregido.
    }

    // Restablecer el valor del Slider al inicio y el contador.
    void ResetTimeSlider()
    {
        if (timeSlider != null)
        {
            timeSlider.value = 0f;
        }

        if (timerText != null)
        {
            timerText.text = "00:00";
        }
    }

    // Nuevo método para detener la reproducción al salir del menú.
    public void ExitMenu()
    {
        StopTrack();  // Detener la reproducción de la pista.
    }
}
