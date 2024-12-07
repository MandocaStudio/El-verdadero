using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public movimientoPlayer player;
    public float healthPlayer;

    public float maxHealthPlayer = 5;

    public bool canDamageRecive;

    public dialogoGeneral dialogoCuracion;

    public bool firstDamage;


    private void Start()
    {
        firstDamage = true;
    }

    private void Update()
    {
        if (healthPlayer <= 0)
        {
            player.irisAnimation.Play("Death");
            StartCoroutine(death());
        }

        if (firstDamage == true && healthPlayer == 4 && dialogoCuracion != null)
        {
            StartCoroutine(activaDialogos3000());
        }


    }

    IEnumerator activaDialogos3000()
    {
        StartCoroutine(dialogoCuracion.empezar());

        yield return new WaitForSeconds(0);

        firstDamage = false;
    }


    public void takeDamage(float damage)
    {
        if (canDamageRecive == true)
        {
            healthPlayer -= damage;

            StartCoroutine(damageColdown());

        }

        if (healthPlayer <= 0)
        {

            player.irisAnimation.Play("Death");
            StartCoroutine(death());

        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("enemyWeapon"))
    //     {
    //         takeDamage(1);

    //     }
    // }

    IEnumerator damageColdown()
    {
        canDamageRecive = false;
        yield return new WaitForSeconds(2f);
        canDamageRecive = true;
    }

    IEnumerator death()
    {
        player.canMove = false;

        float animationDuration = player.irisAnimation["Death"].length;


        yield return new WaitForSeconds(animationDuration);

        player.PanelMuerte.activator = true;
        player.irisAnimation.Stop("Death");

        Time.timeScale = 0;


    }
}
