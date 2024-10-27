using System.Collections;
using UnityEngine;

public class pasos : MonoBehaviour
{
    [Header("Pasos piedra")]
    public AudioClip pasosTile;  // Un solo AudioClip para pasos en piedra

    [Header("Pasos pasto")]
    public AudioClip pasosGrass; // Un solo AudioClip para pasos en pasto

    public AudioSource audioS;

    [SerializeField] private bool isPlaying = false;
    private Coroutine currentCoroutine;  // Guarda la referencia de la corrutina activa

    public movimientoPlayer player;



    private void OnCollisionStay(Collision other)
    {
        Vector3 remappedMovement = new Vector3(player.movement.z, 0, -player.movement.x);

        // Solo empezar la corrutina si no está ya reproduciendo un sonido
        if (other.collider.CompareTag("floorTile") && !isPlaying && remappedMovement != Vector3.zero && player.canMove == true)
        {
            StartCoroutinePasos(pasosPiedra());
        }

        if (other.collider.CompareTag("floorGrass") && !isPlaying && remappedMovement != Vector3.zero && player.canMove == true)
        {
            StartCoroutinePasos(pasosPasto());
        }

        if (remappedMovement == Vector3.zero)
        {
            audioS.Stop();
            isPlaying = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag.Contains("floor"))
        {
            audioS.Stop();

            // Detener la corrutina activa si existe
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;  // Resetea la referencia
                isPlaying = false;
            }


        }
    }

    private void StartCoroutinePasos(IEnumerator coroutine)
    {
        // Detener cualquier corrutina activa antes de iniciar una nueva
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(coroutine);  // Guardar la referencia a la corrutina activa
    }

    IEnumerator pasosPiedra()
    {
        isPlaying = true;

        // Reproducir el paso en piedra
        audioS.PlayOneShot(pasosTile);
        yield return new WaitForSeconds(pasosTile.length);

        isPlaying = false;
    }

    IEnumerator pasosPasto()
    {
        isPlaying = true;

        // Reproducir el paso en pasto
        audioS.PlayOneShot(pasosGrass);
        yield return new WaitForSeconds(pasosGrass.length);

        isPlaying = false;
    }
}

