[System.Serializable]
public class GameSettings
{
    // Opciones de pantalla
    public int visualizationMode; // 0: Pantalla completa, 1: Ventana sin bordes, 2: Ventana
    public float brightness;
    public int resolutionIndex; // Índice para la resolución seleccionada
    
    // Opciones de audio
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float environmentVolume;
}