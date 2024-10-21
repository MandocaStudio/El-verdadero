using UnityEngine;
using UnityEngine.UI;

public class flower : MonoBehaviour
{
    public potion potion;

    public Image flor;

    public Sprite flor5, flor4, flor3, flor2, flor1, flor0;

    void Update()
    {
        if (potion.potionNumber == 5)
        {
            flor.sprite = flor5;
        }
        else if (potion.potionNumber == 4)
        {
            flor.sprite = flor4;
        }
        else if (potion.potionNumber == 3)
        {
            flor.sprite = flor3;
        }
        else if (potion.potionNumber == 2)
        {
            flor.sprite = flor2;
        }
        else if (potion.potionNumber == 1)
        {
            flor.sprite = flor1;
        }
        else if (potion.potionNumber == 0)
        {
            flor.sprite = flor0;
        }
    }
}
