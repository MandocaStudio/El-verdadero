using System.Collections;
using UnityEngine;

public class teleporter : MonoBehaviour
{
    public GameObject player;

    public Transform retorno;

    public movimientoPlayer playerScript;

    public float damage = 1;

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.position = retorno.transform.position;
            playerScript = player.GetComponent<movimientoPlayer>();
            playerScript.healthPlayer.takeDamage(damage);
            StartCoroutine(canMove(playerScript));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))

        {
            player = null;
        }
    }

    IEnumerator canMove(movimientoPlayer player)
    {
        player.canMove = false;
        // aqui va la animacion que sufre daño
        // player.irisAnimation.Play("dance");
        yield return new WaitForSeconds(1F);
        player.isGrounded = true;
        player.canMove = true;

    }
}
