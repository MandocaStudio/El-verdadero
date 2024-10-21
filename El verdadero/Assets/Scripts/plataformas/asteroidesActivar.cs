using System.Collections;
using UnityEngine;

public class asteroidesActivar : MonoBehaviour
{



    [SerializeField] float WaitForSeconds;
    public Animation animationComponent;

    public AudioClip activateSound;

    public AudioSource audioSource;

    [SerializeField] string animationName;

    [SerializeField] bool canActivate, neverAgain;

    // Update is called once per frame
    private void Start()
    {
        canActivate = true;

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("air") && !neverAgain)
        {
            if (canActivate)
            {
                audioSource.clip = activateSound;

                audioSource.Play();
                StartCoroutine(apearPlatform());

            }
        }
    }

    IEnumerator apearPlatform()
    {
        // Obtener todos los clips de animación

        animationComponent.Play(animationName);

        // Esperar la duración de la animación o el tiempo deseado
        yield return new WaitForSeconds(2);


        neverAgain = true;
    }

}
