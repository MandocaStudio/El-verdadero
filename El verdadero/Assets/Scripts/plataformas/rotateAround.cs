using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform centerObject; // El objeto alrededor del cual girará
    public float rotationSpeed = 20f; // Velocidad de rotación
    public Vector3 rotationAxis = Vector3.up; // Eje de rotación (puedes cambiarlo)

    void Update()
    {
        // Haz que el objeto gire alrededor del centro (centerObject) en el eje especificado
        transform.RotateAround(centerObject.position, rotationAxis, rotationSpeed * Time.deltaTime);
    }
}