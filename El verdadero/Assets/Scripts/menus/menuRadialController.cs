using UnityEngine;
using UnityEngine.UI;

public class menuRadialController : MonoBehaviour
{
    public bool canUseRadialMenu = true;
    [Header("Enlace con movimiento")]
    public GameObject player;
    public movimientoPlayer movimiento;

    [Header("Color activo en el menu radial")]
    public float colorActivated;

    [Header("Menu variables y componentes")]
    public Image menuRadial;

    [Header("Variables para movimiento del raton y joystick")]
    public float MouseX, MouseY, joystickX, joystickY;

    public bool isMouseMoving, isUsingMouse, isJoystickMoving, isUsingJoystick;

    public Vector2 lastMouseDirection, lastJoystickDirection, direction;

    public float magnitude;

    [Header("Colores")]

    public CanvasGroup colors;
    public Image verdeImage; //1
    public Image amarilloImage; //2
    public Image azulImage; //5
    public Image cyanImage; //6
    public Image rojoImage; //3
    public Image magentaImage; //4
    private Color rojoColor = new Color32(0xFE, 0x00, 0x00, 0xFF);
    private Color verdeColor = new Color32(0x23, 0xEF, 0x42, 0xFF);
    private Color azulColor = new Color32(0x16, 0x5A, 0xE7, 0xFF);
    private Color amarilloColor = new Color32(0xFF, 0xFF, 0x3C, 0xFF);
    private Color cyanColor = new Color32(0x01, 0xFF, 0xD5, 0xFF);
    private Color magentaColor = new Color32(0xFF, 0x22, 0x90, 0xFF);





    [Header("Centro del menú radial")]
    public RectTransform radialMenuCenter;

    void Start()
    {
        movimiento = player.GetComponent<movimientoPlayer>();

    }

    void Update()
    {
        if (magnitude == 6)
        {
            lastJoystickDirection = new Vector2(0, 0);
            lastMouseDirection = new Vector2(0, 0);
            direction = new Vector2(0, 0);
        }

        if ((Input.GetAxis("LeftTrigger") > 0.5 || Input.GetButton("RadialMenu")) && canUseRadialMenu == true)
        {
            if (magnitude == 6)
            {
                colorActivated = 0;
                verdeImage.color = changeOpacity(0.60f, verdeColor);
                amarilloImage.color = changeOpacity(0.60f, amarilloColor);
                cyanImage.color = changeOpacity(0.60f, cyanColor);
                azulImage.color = changeOpacity(0.60f, azulColor);
                rojoImage.color = changeOpacity(0.60f, rojoColor);
                magentaImage.color = changeOpacity(0.60f, magentaColor);
            }

            joystickX = Input.GetAxis("RightStickHorizontal");
            joystickY = Input.GetAxis("RightStickVertical");

            MouseX = Input.GetAxis("MouseX");
            MouseY = Input.GetAxis("MouseY");

            Vector2 mouseDirection = GetMouseDirection();
            Vector2 joystickDirection = new Vector2(joystickX, joystickY);

            if (mouseDirection.magnitude > 0.1f)
            {
                lastJoystickDirection = new Vector2(0, 0);
                isMouseMoving = true;
                isUsingMouse = true;
                isUsingJoystick = false;
                direction = mouseDirection;
            }
            else
            {
                isMouseMoving = false;
            }

            if (joystickDirection.magnitude > 0.1f)
            {
                lastJoystickDirection = joystickDirection.normalized;
                isJoystickMoving = true;
                isUsingMouse = false;
                isUsingJoystick = true;
                direction = lastJoystickDirection;
            }
            else
            {
                isJoystickMoving = false;
            }

            if (isUsingJoystick || isUsingMouse)
            {
                direction.Normalize();
                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                if (angle < 0)
                {
                    angle += 360;
                }
                int section = Mathf.FloorToInt(angle / 60);
                magnitude = section;
            }
        }

        if (canUseRadialMenu)
        {
            apearsMenuRadial();

        }

        if ((direction.x != 0 || direction.y != 0) && (Input.GetAxis("LeftTrigger") > 0.5f || Input.GetButton("RadialMenu")))
        {
            changeColor();
        }
    }

    private Vector2 GetMouseDirection()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 centerScreen = RectTransformUtility.WorldToScreenPoint(null, radialMenuCenter.position);
        return (mousePosition - centerScreen).normalized;
    }

    public void apearsMenuRadial()
    {
        if (Input.GetAxis("LeftTrigger") > 0.5f || Input.GetButton("RadialMenu"))
        {
            menuRadial.enabled = true;
            colors.alpha = 1;
            //Time.timeScale = 0.25f;
        }
        else if (Input.GetAxis("LeftTrigger") == 0f || Input.GetButtonUp("RadialMenu"))
        {
            menuRadial.enabled = false;
            colors.alpha = 0;
            //Time.timeScale = 1f;
            magnitude = 6;
        }
    }

    public void changeColor()
    {
        if (isUsingJoystick)
        {
            SetAllColorsToLowOpacity();
            direction = new Vector2(0, 0);
            colorActivated = 0;
            switch (magnitude)
            {
                case 0:
                    if (!movimiento.magenta) magentaImage.color = changeOpacity(0.60f, magentaColor);
                    else { colorActivated = 4; magentaImage.color = changeOpacity(1f, magentaColor); }
                    break;
                case 1:
                    if (!movimiento.rojo) rojoImage.color = changeOpacity(0.6f, rojoColor);
                    else { colorActivated = 3; rojoImage.color = changeOpacity(1f, rojoColor); }
                    break;
                case 2:
                    if (!movimiento.amarillo) amarilloImage.color = changeOpacity(0.6f, amarilloColor);
                    else { colorActivated = 2; amarilloImage.color = changeOpacity(1f, amarilloColor); }
                    break;
                case 3:
                    if (!movimiento.verde) verdeImage.color = changeOpacity(0.60f, verdeColor);
                    else { colorActivated = 1; verdeImage.color = changeOpacity(1f, verdeColor); }
                    break;
                case 4:
                    if (!movimiento.cyan) cyanImage.color = changeOpacity(0.6f, cyanColor);
                    else { colorActivated = 6; cyanImage.color = changeOpacity(1f, cyanColor); }
                    break;
                case 5:
                    if (!movimiento.azul) azulImage.color = changeOpacity(0.6f, azulColor);
                    else { colorActivated = 5; azulImage.color = changeOpacity(1f, azulColor); }
                    break;
                default:
                    colorActivated = 0;

                    verdeImage.color = changeOpacity(0.60f, verdeColor);
                    amarilloImage.color = changeOpacity(0.60f, amarilloColor);
                    cyanImage.color = changeOpacity(0.60f, cyanColor);
                    azulImage.color = changeOpacity(0.60f, azulColor);
                    rojoImage.color = changeOpacity(0.60f, rojoColor);
                    magentaImage.color = changeOpacity(0.60f, magentaColor);
                    break;
            }
        }

        if (isUsingMouse)
        {
            SetAllColorsToLowOpacity();
            direction = new Vector2(0, 0);
            colorActivated = 0;
            switch (magnitude)
            {
                case 0:
                    if (!movimiento.verde) verdeImage.color = changeOpacity(0.60f, verdeColor);
                    else { colorActivated = 1; verdeImage.color = changeOpacity(1f, verdeColor); }
                    break;
                case 1:
                    if (!movimiento.amarillo) amarilloImage.color = changeOpacity(0.6f, amarilloColor);
                    else { colorActivated = 2; amarilloImage.color = changeOpacity(1f, amarilloColor); }
                    break;
                case 2:
                    if (!movimiento.rojo) rojoImage.color = changeOpacity(0.6f, rojoColor);
                    else { colorActivated = 3; rojoImage.color = changeOpacity(1f, rojoColor); }
                    break;
                case 3:
                    if (!movimiento.magenta) magentaImage.color = changeOpacity(0.60f, magentaColor);
                    else { colorActivated = 4; magentaImage.color = changeOpacity(1f, magentaColor); }
                    break;
                case 4:
                    if (!movimiento.azul) azulImage.color = changeOpacity(0.6f, azulColor);
                    else { colorActivated = 5; azulImage.color = changeOpacity(1f, azulColor); }
                    break;
                case 5:
                    if (!movimiento.cyan) cyanImage.color = changeOpacity(0.6f, cyanColor);
                    else { colorActivated = 6; cyanImage.color = changeOpacity(1f, cyanColor); }
                    break;
                default:
                    colorActivated = 0;

                    verdeImage.color = changeOpacity(0.60f, verdeColor);
                    amarilloImage.color = changeOpacity(0.60f, amarilloColor);
                    cyanImage.color = changeOpacity(0.60f, cyanColor);
                    azulImage.color = changeOpacity(0.60f, azulColor);
                    rojoImage.color = changeOpacity(0.60f, rojoColor);
                    magentaImage.color = changeOpacity(0.60f, magentaColor);
                    break;
            }
        }
    }

    private Color changeOpacity(float opacity, Color color)
    {
        Color currentColor = color;

        currentColor.r = color.r;
        currentColor.g = color.g;
        currentColor.b = color.b;

        currentColor.a = opacity;

        return currentColor;
    }

    void SetAllColorsToLowOpacity()
    {
        verdeImage.color = changeOpacity(0.60f, verdeColor);
        amarilloImage.color = changeOpacity(0.60f, amarilloColor);
        rojoImage.color = changeOpacity(0.60f, rojoColor);
        magentaImage.color = changeOpacity(0.60f, magentaColor);
        azulImage.color = changeOpacity(0.60f, azulColor);
        cyanImage.color = changeOpacity(0.60f, cyanColor);
    }
}
