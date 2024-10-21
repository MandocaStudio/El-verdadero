using System.Collections;
using UnityEngine;


public class transitionToPLace : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AnimationClip animacionFinal;

    public bool iniciate;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciate == true)
        {
            StartCoroutine(animationActivator());
        }


    }

    IEnumerator animationActivator()
    {
        animator.SetBool("activate", true);

        yield return new WaitForSeconds(animacionFinal.length);



        animator.SetBool("activate", false);

    }


}

