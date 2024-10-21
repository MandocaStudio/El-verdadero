using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class cameras : MonoBehaviour
{

    public CinemachineCamera Museum;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Museum.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Museum.gameObject.SetActive(false);
        }
    }
}