using System.Collections;
using UnityEngine;
public class platformFallingIris : MonoBehaviour
{
    // Posición objetivo

    public pincelAntorcha pincel;

    public Animator animator;

    public movimientoPlayer movimientoPlayerScript;

    public dashPlayer dashPlayerScript;

    public jumpPlayer playerJump;



    void Update()
    {
        if (pincel != null)
        {
            if (pincel.torchUsed == true)
            {
                movimientoPlayerScript.enabled = false;
                dashPlayerScript.enabled = false;
                playerJump.enabled = false;
                animator.SetTrigger("triger");
                pincel.torchUsed = true;

            }

        }

    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            pincel = other.gameObject.GetComponent<pincelAntorcha>();

            movimientoPlayerScript = other.gameObject.GetComponent<movimientoPlayer>();
            dashPlayerScript = other.gameObject.GetComponent<dashPlayer>();
            playerJump = other.gameObject.GetComponent<jumpPlayer>();




            other.transform.SetParent(this.transform);

        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            pincel = null;
            other.transform.SetParent(null);
        }
    }
}

