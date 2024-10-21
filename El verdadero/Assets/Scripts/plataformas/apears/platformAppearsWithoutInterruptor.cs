using System.Collections;
using UnityEngine;

public class platformAppearsWithoutInterruptor : MonoBehaviour
{

    public GameObject platform;

    [SerializeField] float WaitForSeconds;


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(apearPlatform());

    }

    IEnumerator apearPlatform()
    {

        yield return new WaitForSeconds(WaitForSeconds);

        platform.SetActive(true);

    }
}
