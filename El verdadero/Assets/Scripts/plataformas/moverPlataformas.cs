using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Posición objetivo
    public Vector3 targetPositionInitial;
    public Vector3 targetPositionFinal;

    [Header("Distancia Inicial")]
    [SerializeField] float distanceInicialX;
    [SerializeField] float distanceInicialY;
    [SerializeField] float distanceInicialZ;

    [Header("Distancia Final")]

    [SerializeField] float distanceFinalX;

    [SerializeField] float distanceFinalY;

    [SerializeField] float distanceFinalZ;
    // Velocidad de movimiento
    public float speed = 10.0f;

    [SerializeField] bool ifTargetPosition = false;

    [SerializeField] bool isMoving = false;

    void Update()
    {
        if (!ifTargetPosition)
        {
            targetPositionInitial = transform.position + new Vector3(distanceInicialX, distanceInicialY, distanceInicialZ);
            targetPositionFinal = transform.position + new Vector3(distanceFinalZ, distanceFinalY, distanceFinalZ);

            ifTargetPosition = true;
        }
        // Mover el objeto hacia la posición objetivo

        if (isMoving == false)
        {
            StartCoroutine(moverPlataforma());

        }
    }

    IEnumerator moverPlataforma()
    {
        isMoving = true;

        // Mover hacia la posición objetivo
        while (Vector3.Distance(transform.position, targetPositionInitial) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionInitial, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(3);

        // Mover hacia la posición negativa
        while (Vector3.Distance(transform.position, targetPositionFinal) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionFinal, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(3);

        isMoving = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);

        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
