using UnityEngine;
using UnityEngine.UI;

public class inkChanger : MonoBehaviour
{
    public Image inks;

    public Image circle;
    public Sprite red;
    public Sprite green;
    public Sprite blue;

    public menuRadialController colors;

    // Update is called once per frame
    void Update()
    {
        inkShifter();
    }

    private void inkShifter()
    {
        if (colors.colorActivated == 1)
        {
            inks.sprite = green;
            SetAlpha(inks, 1f);
            SetAlpha(circle, 1f);
        }
        else if (colors.colorActivated == 3)
        {
            inks.sprite = red;
            SetAlpha(inks, 1f);
            SetAlpha(circle, 1f);

        }
        else if (colors.colorActivated == 5)
        {
            inks.sprite = blue;
            SetAlpha(inks, 1f);
            SetAlpha(circle, 1f);

        }
        else if (colors.colorActivated == 0)
        {
            SetAlpha(inks, 0f);
            SetAlpha(circle, 0.5f);
        }
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
