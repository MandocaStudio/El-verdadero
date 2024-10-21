using System.Collections;
using UnityEngine;

public class giveHabilitiesVerde : MonoBehaviour
{
    [SerializeField] bool canUse;

    public menuRadialController menu;

    public movimientoPlayer player;

    [SerializeField] bool neverAgain;

    public GameObject particlesContainer;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && canUse == true && neverAgain == false)
        {
            StartCoroutine(giveColor());

            neverAgain = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canUse = true;

            // Transform child = other.transform.GetChild(3);

            // Transform grandChild = child.GetChild(2);

            // menu = grandChild.GetComponent<menuRadialController>();

            Transform child = other.transform.Find("hud");

            Transform grandChild = child.Find("menuRadial");

            menu = grandChild.GetComponent<menuRadialController>();

            player = other.GetComponent<movimientoPlayer>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canUse = false;
            menu = null;
            player = null;


        }
    }

    IEnumerator giveColor()
    {
        player.canMove = false;

        // AnimationClip animation = player.irisAnimation.GetClip("iniciodeljuego");
        // player.irisAnimation.Play("iniciodeljuego");

        // float animationDuration = animation.length;

        particlesContainer.SetActive(true);

        yield return new WaitForSeconds(4f);

        menu.canUseRadialMenu = true;
        player.verde = true;
        player.canMove = true;

    }
}
