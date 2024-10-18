using UnityEngine;
using TMPro; // Asegúrate de incluir el espacio de nombres de TextMeshPro
using System.Collections.Generic;

public class VisualizationManager : MonoBehaviour
{
    public TMP_Dropdown visualizationDropdown; // Dropdown para modo de visualización

    void Start()
    {
        // Verifica que el Dropdown esté asignado antes de inicializar
        if (visualizationDropdown == null)
        {
            Debug.LogError("Dropdown de modo de visualización no asignado en el inspector.");
            return;
        }

        InitializeVisualizationDropdown();
    }

    public void OnVisualizationChange(int modeIndex)
    {
        ApplyVisualizationMode(modeIndex);
        PlayerPrefs.SetInt("visualizationMode", modeIndex);
        PlayerPrefs.Save();
    }

    private void ApplyVisualizationMode(int modeIndex)
    {
        FullScreenMode mode = FullScreenMode.Windowed;

        switch (modeIndex)
        {
            case 0:
                mode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                mode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                mode = FullScreenMode.Windowed;
                break;
            default:
                Debug.LogError("Modo de visualización no válido.");
                return;
        }

        Screen.fullScreenMode = mode;
    }

    private void InitializeVisualizationDropdown()
    {
        visualizationDropdown.ClearOptions();
        List<string> options = new List<string>
        {
            "Pantalla Completa",
            "Ventana sin Bordes",
            "Ventana"
        };
        visualizationDropdown.AddOptions(options);

        int savedMode = PlayerPrefs.GetInt("visualizationMode", 0); // Default Pantalla Completa
        ApplyVisualizationMode(savedMode);

        visualizationDropdown.value = savedMode;
        visualizationDropdown.RefreshShownValue();

        visualizationDropdown.onValueChanged.AddListener(delegate { OnVisualizationChange(visualizationDropdown.value); });
    }
}
