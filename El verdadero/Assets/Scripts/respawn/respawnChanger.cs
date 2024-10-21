using UnityEngine;

public class respawnChanger : MonoBehaviour
{

    public Transform respawnObject;

    public teleporter teleporter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teleporter.retorno = respawnObject;
        }
    }
}
