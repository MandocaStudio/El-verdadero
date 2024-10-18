using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f; // Relación de aspecto deseada (16:9).

    void Start()
    {
        AdjustCameraAspectRatio(); // Ajustar el aspecto de la cámara al iniciar la escena.
    }

    void AdjustCameraAspectRatio()
    {
        // Verifica si la cámara existe en este objeto.
        Camera camera = GetComponent<Camera>();
        if (camera == null)
        {
            Debug.LogWarning("No se encontró la cámara en este objeto. Asegúrate de que el script esté en la cámara.");
            return;
        }

        float windowAspectRatio = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspectRatio / targetAspectRatio;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
