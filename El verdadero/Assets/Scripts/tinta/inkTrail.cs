using Unity.Mathematics;
using UnityEngine;

public class inkTrail : MonoBehaviour
{

    [Header("Variables de spawn")]
    public GameObject azul;

    public GameObject rojo;
    public GameObject verde;


    public GameObject player;

    public movimientoPlayer playerMovement;

    public float spawnInterval = 0.1f;
    public float nextSpawnTime;

    [Header("variables de cambio de tinta")]

    public GameObject menuRadial;
    public menuRadialController color;
    void Start()
    {
        color = menuRadial.GetComponent<menuRadialController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button5) && playerMovement.isGrounded == true && playerMovement.canMove == true)
        {
            if (Time.time >= nextSpawnTime)
            {
                spawn();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
    }

    public void spawn()
    {
        Vector3 spawnPosition = new Vector3(player.transform.position.x, player.transform.position.y - 0.557f, player.transform.position.z);
        float x = 90f;
        quaternion rotation = Quaternion.Euler(x, 0, 0);

        if (color.colorActivated == 1)
        {
            Instantiate(verde, spawnPosition, rotation);

        }

        else if (color.colorActivated == 3)
        {
            Instantiate(rojo, spawnPosition, rotation);

        }

        else if (color.colorActivated == 5)
        {
            Instantiate(azul, spawnPosition, rotation);

        }




    }
}
