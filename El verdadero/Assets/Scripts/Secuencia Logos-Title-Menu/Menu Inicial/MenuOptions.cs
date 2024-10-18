using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private Slider environmentSlider;
    [SerializeField] private Slider brightnessSlider;

    private void Start()
    {
        LoadSettings();

        // Suscribirse a los eventos de cambio de valor
        masterSlider.onValueChanged.AddListener(ChangeVolumeMaster);
        musicSlider.onValueChanged.AddListener(ChangeVolumeMusic);
        soundEffectsSlider.onValueChanged.AddListener(ChangeVolumeSoundEffects);
        environmentSlider.onValueChanged.AddListener(ChangeVolumeEnvironment);
        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);
    }

 public void ChangeBrightness(float brightness)
    {
        // Ajusta el brillo global o un panel de brillo
        ApplyBrightness(brightness);
        SettingsManager.Instance.settings.brightness = brightness;
        SettingsManager.Instance.SaveSettings();
    }

    private void ApplyBrightness(float brightness)
    {
        GameObject brightnessPanel = GameObject.Find("BrilloPanel");
        if (brightnessPanel != null)
        {
            Image panelImage = brightnessPanel.GetComponent<Image>();
            if (panelImage != null)
            {
                // Ajustar el alfa del color para cambiar el brillo
                Color newColor = panelImage.color;
                newColor.a = 1.0f - brightness; // Invertir el valor si 1.0 es brillo máximo
                panelImage.color = newColor;
            }
        }
    }
    public void ChangeVolumeMaster(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        if (SettingsManager.Instance.settings != null)
        {
            SettingsManager.Instance.settings.masterVolume = volume;
            SettingsManager.Instance.SaveSettings();
        }
    }

    public void ChangeVolumeMusic(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        if (SettingsManager.Instance.settings != null)
        {
            SettingsManager.Instance.settings.musicVolume = volume;
            SettingsManager.Instance.SaveSettings();
        }
    }

    public void ChangeVolumeSoundEffects(float volume)
    {
        audioMixer.SetFloat("SoundEffectsVolume", volume);
        if (SettingsManager.Instance.settings != null)
        {
            SettingsManager.Instance.settings.sfxVolume = volume;
            SettingsManager.Instance.SaveSettings();
        }
    }

    public void ChangeVolumeEnvironment(float volume)
    {
        audioMixer.SetFloat("EnvironmentVolume", volume);
        if (SettingsManager.Instance.settings != null)
        {
            SettingsManager.Instance.settings.environmentVolume = volume;
            SettingsManager.Instance.SaveSettings();
        }
    }

    private void LoadSettings()
    {
        if (SettingsManager.Instance != null && SettingsManager.Instance.settings != null)
        {
            masterSlider.value = SettingsManager.Instance.settings.masterVolume;
            audioMixer.SetFloat("MasterVolume", SettingsManager.Instance.settings.masterVolume);

            musicSlider.value = SettingsManager.Instance.settings.musicVolume;
            audioMixer.SetFloat("MusicVolume", SettingsManager.Instance.settings.musicVolume);

            soundEffectsSlider.value = SettingsManager.Instance.settings.sfxVolume;
            audioMixer.SetFloat("SoundEffectsVolume", SettingsManager.Instance.settings.sfxVolume);

            environmentSlider.value = SettingsManager.Instance.settings.environmentVolume;
            audioMixer.SetFloat("EnvironmentVolume", SettingsManager.Instance.settings.environmentVolume);

            brightnessSlider.value = SettingsManager.Instance.settings.brightness;
            RenderSettings.ambientLight = Color.white * SettingsManager.Instance.settings.brightness;
        }
        else
        {
            Debug.LogWarning("SettingsManager or its settings are not initialized.");
        }
    }
}