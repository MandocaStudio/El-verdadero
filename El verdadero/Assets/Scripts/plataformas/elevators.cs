using System.Collections;
using UnityEngine;

public class Elevators : MonoBehaviour
{
    public bool moving;
    public float speed = 5f;
    public Transform platformTransform;

    public float moveDistance = 6f; // Define the distance the elevator should move

    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 finalPosition;

    [SerializeField] private bool direction = true;

    [SerializeField] private bool coroutineRunning;

    void Start()
    {
        initialPosition = platformTransform.position;



        platformTransform.position = initialPosition;

    }

    private void Update()
    {
        if (direction == true)
        {
            finalPosition = initialPosition + new Vector3(0, moveDistance, 0); // Move up by the specified distance

        }
        else
        {
            finalPosition = initialPosition - new Vector3(0, moveDistance, 0); // Move up by the specified distance

        }

        if (moving == true && coroutineRunning == false)
        {
            StartCoroutine(ElevatorAction());

        }

    }
    IEnumerator ElevatorAction()
    {
        coroutineRunning = true;
        while (Vector3.Distance(platformTransform.position, finalPosition) > 0.01f)
        {
            platformTransform.position = Vector3.MoveTowards(platformTransform.position, finalPosition, speed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        platformTransform.position = finalPosition;

        yield return new WaitForSeconds(2f); // Pause at the final position

        moving = false;
        direction = !direction; // Change direction after the movement is completed
        initialPosition = platformTransform.position; // Update initialPosition for the next movement

        coroutineRunning = false;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.parent = platformTransform; // Attach player to the platform
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.parent = null; // Detach player from the platform
        }
    }
}
