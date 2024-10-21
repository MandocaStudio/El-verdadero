using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class createAfterDefeatEnemies : MonoBehaviour
{

    public GameObject araña1, araña2;

    public GameObject platforms;
    public Animator animatorPlatforms;

    public CinemachineCamera camara;

    public AudioClip soundtrack;

    public AudioSource audioSource;

    public bool activar;

    public bool neverAgain;

    // Update is called once per frame
    void Update()
    {
        if (araña1 == null && araña2 == null && neverAgain == false)
        {
            activar = true;
        }

        if (activar == true && neverAgain == false)
        {
            neverAgain = true;

            StartCoroutine(activate());
        }
    }

    IEnumerator activate()
    {
        camara.Priority = 2;
        audioSource.Stop();
        audioSource.clip = soundtrack;

        yield return new WaitForSeconds(3);

        // Activar plataformas y la animación
        platforms.SetActive(true);
        animatorPlatforms.SetTrigger("activate");

        // Esperar un frame para asegurarte de que la animación comience
        yield return null;

        // Obtener la duración del clip de animación actual
        AnimatorClipInfo[] clipInfo = animatorPlatforms.GetCurrentAnimatorClipInfo(0);
        float animationDuration = clipInfo[0].clip.length;

        // Esperar la duración de la animación
        yield return new WaitForSeconds(animationDuration);

        // Cambiar la prioridad de la cámara después de la animación
        camara.Priority = 0;

        audioSource.Play();
    }

}
