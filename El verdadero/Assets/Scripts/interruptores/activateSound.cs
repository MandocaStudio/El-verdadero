using System.Collections;
using UnityEngine;

public class activateSound : MonoBehaviour
{



    [SerializeField] float WaitForSeconds;


    public AudioClip SoundClip;

    public AudioSource audioSource;

    [SerializeField] string objectCollider;



    [SerializeField] bool canActivate, neverAgain;

    // Update is called once per frame
    private void Start()
    {
        canActivate = true;

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(objectCollider) && !neverAgain)
        {
            if (canActivate)
            {
                audioSource.clip = SoundClip;

                audioSource.Play();

                Destroy(gameObject);

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectCollider) && !neverAgain)
        {
            if (canActivate)
            {
                audioSource.clip = SoundClip;

                audioSource.Play();

                Destroy(gameObject);

            }

        }
    }

}

