using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class bucleIdle : MonoBehaviour
{
    public Animation irisAnimation;

    public GameObject hud, potion;

    public movimientoPlayer player;
    public atackGestion attack;
    public dashPlayer dash;

    [SerializeField] bool inicio, inicioRutina;

    //public CinemachineOrbitalFollow freeLookCamera;


    void Start()
    {
        player.enabled = false;
        dash.enabled = false;
        attack.enabled = false;

        hud.SetActive(false);
        potion.SetActive(false);

        inicio = true;
        inicioRutina = true;

        //freeLookCamera.enabled = false;
    }

    IEnumerator startDelay()
    {
        irisAnimation.Play("iniciodeljuego");
        float animDuration = irisAnimation["iniciodeljuego"].clip.length;

        // Esperamos hasta que la animación de "inicio del juego" termine
        yield return new WaitForSeconds(animDuration);

        // Habilitamos las funciones del jugador
        player.enabled = true;
        dash.enabled = true;
        attack.enabled = true;

        // Activamos el HUD y otros objetos
        hud.SetActive(true);
        potion.SetActive(true);

        //freeLookCamera.enabled = true;

    }

    IEnumerator startBucle()
    {
        irisAnimation.Play("bucle");

        float animDuration = irisAnimation["bucle"].clip.length;

        yield return new WaitForSeconds(animDuration);
    }

    void Update()
    {


        if (Input.GetButtonDown("Interact") && inicio)
        {

            inicio = false;
            inicioRutina = false;

            irisAnimation.Stop("bucle");

            StopAllCoroutines();

            StartCoroutine(startDelay());
        }

        // Si el botón "Interact" no ha sido presionado y estamos en la rutina de inicio
        if (!Input.GetButtonDown("Interact") && inicio && inicioRutina)
        {

            StartCoroutine(startBucle());

        }

    }
}
