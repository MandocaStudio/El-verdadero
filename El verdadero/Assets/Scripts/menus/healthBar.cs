using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public menuRadialController colors;

    public health playerHealth;

    public Image healthBarImage;

    public Image brushBar;

    [Header("barras de vida")]
    public Sprite blueBar;
    public Sprite greenBar;
    public Sprite orangeBar;
    public Sprite redBar;

    [Header("pinceles")]
    public Sprite brush;
    public Sprite redBrush;
    public Sprite blueBrush;
    public Sprite greenBrush;


    // Update is called once per frame
    void Update()
    {
        barController(colors.colorActivated, playerHealth.healthPlayer);
    }

    private void barController(float color, float healt)
    {
        //bar colors controller
        if (color == 0) //nada
        {
            healthBarImage.sprite = orangeBar;
            brushBar.sprite = brush;
        }
        else if (color == 1) //verde
        {
            healthBarImage.sprite = greenBar;
            brushBar.sprite = greenBrush;
        }
        else if (color == 5) //azul
        {
            healthBarImage.sprite = blueBar;
            brushBar.sprite = blueBrush;
        }
        else if (color == 3) //rojo
        {
            healthBarImage.sprite = redBar;
            brushBar.sprite = redBrush;
        }

        //Amount of life controller

        if (healt == 5)
        {
            healthBarImage.fillAmount = 1;

        }
        else if (healt == 4)
        {
            healthBarImage.fillAmount = 0.76f;
        }
        else if (healt == 3)
        {
            healthBarImage.fillAmount = 0.51f;
        }
        else if (healt == 2)
        {
            healthBarImage.fillAmount = 0.34f;
        }
        else if (healt == 1)
        {
            healthBarImage.fillAmount = 0.17f;
        }
        else if (healt == 0)
        {
            healthBarImage.fillAmount = 0f;
        }

    }
}
