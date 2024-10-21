using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class atackGestion : MonoBehaviour
{
    public Animator animator;

    public bool canAtack = true;

    public int comboIndicator = 1;

    [SerializeField] bool Combo = false;

    [SerializeField] float ComboTime = 0;

    private string animationName = "Attack1";

    public movimientoPlayer player;

    void Update()
    {



        if (Combo == true)
        {
            ComboTime += Time.deltaTime;
        }

        if (ComboTime >= 2f)
        {
            Combo = false;
            ComboTime = 0;
        }




        if (Input.GetButtonDown("Attack") && canAtack == true && player.isGrounded == true)
        {


            if (Combo == true)
            {
                comboIndicator = (comboIndicator % 3) + 1;
            }
            else if (Combo == false)
            {
                comboIndicator = 1;
            }
            animationName = "Attack1";

            // + comboIndicator;


            player.irisAnimation.Play(animationName);


            StartCoroutine(atack());

        }
    }

    IEnumerator atack()
    {
        player.canMove = false;
        canAtack = false;

        float animationDuration = player.irisAnimation[animationName].length;

        Debug.Log(animationDuration);

        yield return new WaitForSeconds(animationDuration);

        player.canMove = true;
        canAtack = true;

        Combo = true;
        ComboTime = 0;

    }

}
