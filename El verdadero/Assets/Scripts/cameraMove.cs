using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public Vector3 lastLocation;
    public bool isWithinBounds = true;

    // Update is called once per frame
    void Update()
    {

        if (isWithinBounds == true)
        {
            // La cámara sigue al jugador
            transform.position = player.transform.position + new Vector3(-13, 10, -13);
        }
        else
        {
            // La cámara permanece en la última ubicación válida
            transform.position = lastLocation;
        }
    }


}