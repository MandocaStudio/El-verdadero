using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown Dropdownresolution;
    public TMP_Dropdown DropdownWindowMode;

    private Resolution[] resolutions = new Resolution[]
    {
        new Resolution { width = 1920, height = 1200 },
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1680, height = 1050 },
        new Resolution { width = 1280, height = 720 }
    };

    private int currentResolutionIndex;

    public static ResolutionManager Instance { get; private set; }

    public delegate void ResolutionChanged();
    public static event ResolutionChanged OnResolutionChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (Dropdownresolution == null || DropdownWindowMode == null)
        {
            Debug.LogError("Dropdown de resolución o de modo de ventana no asignado en el inspector.");
            return;
        }

        InitializeDropdown();
    }

    public void OnResolutionChange(int resolutionIndex)
    {
        ApplyResolution(resolutionIndex);
        SettingsManager.Instance.settings.resolutionIndex = resolutionIndex;
        SettingsManager.Instance.SaveSettings();
        OnResolutionChanged?.Invoke();
    }

    public void OnWindowModeChange(int modeIndex)
    {
        ApplyWindowMode(modeIndex);
        SettingsManager.Instance.settings.visualizationMode = modeIndex;
        SettingsManager.Instance.SaveSettings();
    }

    public void ApplyResolution(int resolutionIndex)
    {
        if (resolutions == null || resolutions.Length == 0)
        {
            Debug.LogError("Las resoluciones no han sido inicializadas.");
            return;
        }

        if (resolutionIndex < 0 || resolutionIndex >= resolutions.Length)
        {
            Debug.LogError("Índice de resolución no válido.");
            return;
        }

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void ApplyWindowMode(int modeIndex)
    {
        switch (modeIndex)
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

    private void InitializeDropdown()
    {
        currentResolutionIndex = SettingsManager.Instance.settings.resolutionIndex;

        if (resolutions.Length > 0)
        {
            ApplyResolution(currentResolutionIndex);
        }
        else
        {
            Debug.LogError("Las resoluciones no pudieron ser inicializadas.");
        }

        Dropdownresolution.ClearOptions();
        List<string> options = new List<string>();
        foreach (Resolution resolution in resolutions)
        {
            options.Add(resolution.width + " x " + resolution.height);
        }
        Dropdownresolution.AddOptions(options);
        Dropdownresolution.value = currentResolutionIndex;
        Dropdownresolution.RefreshShownValue();
        Dropdownresolution.onValueChanged.AddListener(delegate { OnResolutionChange(Dropdownresolution.value); });

        // Initialize window mode dropdown
        DropdownWindowMode.ClearOptions();
        List<string> windowModes = new List<string> { "Pantalla completa", "Ventana sin bordes", "Ventana" };
        DropdownWindowMode.AddOptions(windowModes);
        DropdownWindowMode.value = SettingsManager.Instance.settings.visualizationMode;
        DropdownWindowMode.RefreshShownValue();
        DropdownWindowMode.onValueChanged.AddListener(delegate { OnWindowModeChange(DropdownWindowMode.value); });
    }
}
