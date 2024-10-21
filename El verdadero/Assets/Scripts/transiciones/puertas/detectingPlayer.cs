using UnityEngine;

public class detectingPlayer : MonoBehaviour
{
    public bool playerIsHere = false;


    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            playerIsHere = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            playerIsHere = false;
        }
    }
}
