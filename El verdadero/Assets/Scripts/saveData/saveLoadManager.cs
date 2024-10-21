using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/playerData.json";
    }

    public void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Datos guardados: " + json);
    }

    public PlayerData LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Datos cargados: " + json);
            return data;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún archivo de datos.");
            return null;
        }
    }
}
