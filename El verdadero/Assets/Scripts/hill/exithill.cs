using UnityEngine;

public class exithill : MonoBehaviour
{
    public deslizamiento container;

    private void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            container.player.isSliding = false;
        }
    }
}
