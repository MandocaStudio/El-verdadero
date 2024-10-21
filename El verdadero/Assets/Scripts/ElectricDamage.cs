using System.Collections;
using UnityEngine;

public class ElectricDamage : MonoBehaviour
{
    public Collider colision;

    public bool activar = true;

    public float activateTimmer = 0;

    public Renderer material;

    public Color newColor;

    public Transform retorn;

    void Start()
    {
        material = GetComponent<Renderer>();
    }
    void Update()
    {
        activateTimmer += Time.deltaTime;

        if (activateTimmer >= 2.5f)
        {
            activar = !activar;
            activateTimmer = 0;

            colision.enabled = activar;
            if (activar == false && ColorUtility.TryParseHtmlString("#B5F3FA", out newColor))
            {
                material.material.color = newColor;
            }
            else if (activar == true && ColorUtility.TryParseHtmlString("#FF5733", out newColor))
            {
                material.material.color = newColor;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health playerHealth = other.GetComponent<health>();

            Transform playerPosition = other.GetComponent<Transform>();

            if (playerHealth != null && activar == true)
            {
                playerHealth.takeDamage(1);

                StartCoroutine(noMove());
                playerPosition.position = retorn.position;
            }
        }
    }

    IEnumerator noMove()
    {
        movimientoPlayer playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<movimientoPlayer>();

        playerMovement.canMove = false;
        yield return new WaitForSeconds(1f);
        playerMovement.canMove = true;

    }
}
