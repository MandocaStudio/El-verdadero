using System.Collections;
using UnityEngine;

public class particlesToPlayer : MonoBehaviour
{
    public GameObject colorParticle;

    public bool activate;

    [SerializeField] float retrazo;

    [Header("Move Player")]
    [SerializeField] float speed;

    public GameObject pincel;

    private void Start()
    {
        retrazo = Random.Range(0, 2.5f);
        activate = true;

        // Inicia la destrucción 4 segundos después de la activación
        StartCoroutine(destroyAfterTime(4f));
    }

    private void Update()
    {
        if (activate == true)
        {
            StartCoroutine(toPlayer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pincel"))
        {
            // Si entra en contacto con el pincel, se puede destruir antes
            StartCoroutine(destroyParticle());
        }
    }

    IEnumerator destroyParticle()
    {
        // Espera 2 segundos antes de destruir después de colisionar
        yield return new WaitForSeconds(2);
        Destroy(colorParticle);
    }

    // Corrutina para destruir después de 4 segundos de estar activo
    IEnumerator destroyAfterTime(float seconds)
    {
        // Espera la cantidad de segundos antes de destruir
        yield return new WaitForSeconds(seconds);

        Destroy(colorParticle);  // Destruye la partícula después del tiempo dado
    }

    IEnumerator toPlayer()
    {
        // Retrasa el movimiento hacia el jugador
        yield return new WaitForSeconds(retrazo);

        // Mueve la partícula hacia el pincel
        transform.position = Vector3.MoveTowards(transform.position, pincel.transform.position, speed * Time.deltaTime);
    }
}
