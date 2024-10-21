using UnityEngine;

public class inactivador : MonoBehaviour
{

    public dialogueZoneActivator dialogo;

    void Start()
    {
        dialogo.enabled = false;
    }


}
