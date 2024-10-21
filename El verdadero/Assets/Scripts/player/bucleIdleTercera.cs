using System.Collections;
using UnityEngine;

public class bucleIdleTercera : MonoBehaviour
{
    public Animator irisAnimator;  // Cambiado a Animator
    public GameObject hud, potion;
    public movimientoPlayer player;
    public atackGestion attack;
    public dashPlayer dash;

    [SerializeField] bool inicioRutina = true;

    void Start()
    {
        // Desactivar funciones del jugador al inicio
        player.enabled = false;
        dash.enabled = false;
        attack.enabled = false;

        hud.SetActive(false);
        potion.SetActive(false);

        // Iniciar la animación y la secuencia
        StartCoroutine(startDelay());
    }

    IEnumerator startDelay()
    {
        // Reproducir animación de inicio con SetTrigger
        irisAnimator.SetTrigger("triger");

        // Obtener la duración de la animación
        float animDuration = irisAnimator.GetCurrentAnimatorStateInfo(0).length;

        // Esperar hasta que la animación de "iniciodeljuego" termine
        yield return new WaitForSeconds(animDuration);

        // Habilitar las funciones del jugador
        player.enabled = true;
        dash.enabled = true;
        attack.enabled = true;

        hud.SetActive(true);
        potion.SetActive(true);

        // Iniciar el bucle de animación
    }


}
