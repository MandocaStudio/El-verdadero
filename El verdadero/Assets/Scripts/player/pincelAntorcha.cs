using UnityEngine;

public class pincelAntorcha : MonoBehaviour
{

    public menuRadialController menuRadial;

    public Light torch;

    public bool canUseTorch;

    public bool torchUsed;


    void Update()
    {
        if (menuRadial.colorActivated == 3)
        {
            canUseTorch = true;
            useTorch();
        }
        else if (menuRadial.colorActivated != 3 || (Input.GetAxis("RightTrigger") >= 0.5f || Input.GetButton("Hability")))
        {
            canUseTorch = false;
            torch.enabled = false;
        }
    }

    private void useTorch()
    {
        if (canUseTorch == true && (Input.GetAxis("RightTrigger") >= 0.5f || Input.GetButton("Hability")))
        {
            torch.enabled = true;

            torchUsed = true;
        }
        else if (canUseTorch == true && (Input.GetAxis("RightTrigger") < 0.5f || Input.GetButtonUp("Hability")))
        {
            torch.enabled = false;

            torchUsed = false;

        }
    }
}
