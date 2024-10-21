using System.Collections;
using UnityEngine;

public class platformComponentsActivate : MonoBehaviour
{

    public GameObject platform;

    [SerializeField] float WaitForSeconds;

    public BoxCollider colliderB;

    public MeshRenderer mesh;

    public bool activar;


    // Update is called once per frame
    void Update()
    {
        if (activar == true)
        {
            StartCoroutine(activateComponents());

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("air"))
        {
            StartCoroutine(activateComponents());
        }
    }

    IEnumerator activateComponents()
    {

        yield return new WaitForSeconds(WaitForSeconds);

        colliderB.enabled = true;
        mesh.enabled = true;

    }
}
