using System.Collections;
using UnityEngine;

public class platformApearsWithoutAnimation : MonoBehaviour
{

    public GameObject platform;

    [SerializeField] float WaitForSeconds;


    public bool activar;

    // Update is called once per frame
    void Update()
    {
        if (activar == true)
        {
            StartCoroutine(apearPlatform());

        }
    }

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("air"))
        {
            StartCoroutine(apearPlatform());
        }
    }

    IEnumerator apearPlatform()
    {


        yield return new WaitForSeconds(WaitForSeconds);

        platform.SetActive(true);

    }
}
