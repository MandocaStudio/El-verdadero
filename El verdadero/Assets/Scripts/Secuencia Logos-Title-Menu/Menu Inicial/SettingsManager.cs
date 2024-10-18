using UnityEngine;
using System.IO;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public GameSettings settings;
    public AudioMixer audioMixer;
    private string settingsFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            settingsFilePath = Path.Combine(Application.persistentDataPath, "gamesettings.json");
            LoadSettings();
            ApplySettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(settingsFilePath, json);
    }

    public void LoadSettings()
    {
        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            settings = JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            settings = new GameSettings(); // Crear una nueva configuración si no existe
        }
    }

    public void ApplySettings()
    {
        if (settings == null)
        {
            Debug.LogError("Settings are not loaded.");
            return;
        }

        ApplyBrightness(settings.brightness);
        audioMixer.SetFloat("MasterVolume", settings.masterVolume);
        audioMixer.SetFloat("MusicVolume", settings.musicVolume);
        audioMixer.SetFloat("SoundEffectsVolume", settings.sfxVolume);
        audioMixer.SetFloat("EnvironmentVolume", settings.environmentVolume);
        ApplyResolution(settings.resolutionIndex);
        ApplyVisualizationMode(settings.visualizationMode);
    }

    private void ApplyBrightness(float brightness)
    {
        GameObject brightnessPanel = GameObject.Find("BrilloPanel");
        if (brightnessPanel != null)
        {
            Image panelImage = brightnessPanel.GetComponent<Image>();
            if (panelImage != null)
            {
                panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, brightness);
            }
        }
    }

    private void ApplyResolution(int resolutionIndex)
    {
        // Asegúrate de que ResolutionManager está correctamente configurado y no es nulo
        if (ResolutionManager.Instance != null)
        {
            ResolutionManager.Instance.ApplyResolution(resolutionIndex);
        }
        else
        {
            Debug.LogError("ResolutionManager instance not found.");
        }
    }

    private void ApplyVisualizationMode(int mode)
    {
        switch (mode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
}