using UnityEngine;

public class vfxColorChanger : MonoBehaviour
{

    public GameObject vfxRed, vfxBlue, vfxGreen;

    //, vfxMagenta, vfxCyan, vfxYellow;

    public menuRadialController colors;

    [Header("Materiales del pincel")]

    public Material redBrush;
    public Material greenBrush;
    public Material blueBrush;

    public Material voidBrush;
    public Renderer brushRenderer;

    private void Update()
    {
        switch (colors.colorActivated)
        {
            case 1:
                vfxGreen.SetActive(true);
                brushRenderer.material = greenBrush;
                break;

            // case 2:
            //     vfxYellow.SetActive(true);
            //brushRenderer.material = 
            //     break;

            case 3:
                vfxRed.SetActive(true);
                brushRenderer.material = redBrush;
                break;

            // case 4:
            //     vfxMagenta.SetActive(true);
            //brushRenderer.material = 
            //     break;

            case 5:
                vfxBlue.SetActive(true);
                brushRenderer.material = blueBrush;
                break;

            // case 6:
            //     vfxCyan.SetActive(true);
            //brushRenderer.material = 
            //     break;
            default:
                // // vfxCyan.SetActive(false);
                vfxBlue.SetActive(false);
                // // vfxMagenta.SetActive(false);
                vfxRed.SetActive(false);
                // // vfxYellow.SetActive(false);
                vfxGreen.SetActive(false);

                brushRenderer.material = voidBrush;
                break;
        }
    }
}
