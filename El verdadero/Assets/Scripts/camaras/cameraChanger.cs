using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class cameraChanger : MonoBehaviour
{

    public CinemachineCamera cameraPuzle;


    [SerializeField] string gameObjectCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(gameObjectCollider))
        {
            cameraPuzle.Priority = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(gameObjectCollider))
        {
            cameraPuzle.Priority = 0;

        }
    }
}