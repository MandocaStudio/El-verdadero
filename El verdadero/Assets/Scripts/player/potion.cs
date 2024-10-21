using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class potion : MonoBehaviour
{
    public bool canUsePotion = true;

    [SerializeField] bool potionDelay = false;
    public float potionNumber = 5;
    public float healtRestored = 1;

    public health playerLife;

    [Header("Flor Sprites")]
    public Image flower;

    public Sprite completeFlower;
    public Sprite fourPiecesFlower;
    public Sprite threePiecesFlower;
    public Sprite twoPiecesFlower;
    public Sprite onePieceFlower;
    public Sprite nothingPiecesFlower;

    [Header("VFX")]
    public GameObject healingVFX;


    private void Update()
    {
        flowerChanger(potionNumber);
        if (Input.GetButtonDown("UseObject"))
        {
            if (potionDelay == true)
            {
                return;
            }
            usePotion();

        }
    }

    public void usePotion()
    {

        if (potionNumber == 0 || potionDelay == true)
        {
            canUsePotion = false;
            return;
        }
        else if (potionNumber > 0 && potionDelay == false)
        {
            canUsePotion = true;
        }


        if (canUsePotion == true)
        {

            if (playerLife.healthPlayer < playerLife.maxHealthPlayer)
            {
                StartCoroutine(vfxActivator());
                StartCoroutine(potionColdown());
                playerLife.healthPlayer += healtRestored;
                potionNumber -= 1;


            }

        }

        if (potionNumber == 0)
        {
            canUsePotion = false;
        }
    }

    IEnumerator vfxActivator()
    {
        healingVFX.SetActive(true);
        yield return new WaitForSeconds(2f);
        healingVFX.SetActive(false);
    }

    IEnumerator potionColdown()
    {
        canUsePotion = false;
        potionDelay = true;
        yield return new WaitForSeconds(10);
        canUsePotion = true;
        potionDelay = false;
    }
    private void flowerChanger(float potionNumber)
    {
        switch (potionNumber)
        {
            case 5:
                flower.sprite = completeFlower;
                break;

            case 4:
                flower.sprite = fourPiecesFlower;

                break;

            case 3:
                flower.sprite = threePiecesFlower;
                break;

            case 2:
                flower.sprite = twoPiecesFlower;
                break;

            case 1:
                flower.sprite = onePieceFlower;
                break;


            default:
                flower.sprite = nothingPiecesFlower;
                break;
        }

    }
}
