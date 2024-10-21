using System.Collections;
using UnityEngine;
public class movePlatformsWithCandle : MonoBehaviour
{
    // Posición objetivo
    [SerializeField] Vector3 targetPositionInitial;
    [SerializeField] Vector3 targetPositionFinal;



    [SerializeField] float speed;

    public pincelAntorcha pincel;

    [SerializeField] bool back = false;


    void Update()
    {
        if (pincel != null)
        {
            if (pincel.torchUsed == true)
            {
                Vector3 targetLocalPosition = transform.parent.InverseTransformPoint(targetPositionFinal);

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocalPosition, speed * Time.deltaTime);


            }

        }


        if (back == true)
        {
            StartCoroutine(backState());
        }

    }

    IEnumerator backState()
    {
        yield return new WaitForSeconds(3);
        Vector3 targetLocalPosition = transform.parent.InverseTransformPoint(targetPositionInitial);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocalPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, targetLocalPosition) < 0.01f)
        {
            back = false;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            pincel = other.gameObject.GetComponent<pincelAntorcha>();
            other.transform.SetParent(this.transform);

            StopAllCoroutines();
            back = false;

        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            pincel = null;
            other.transform.SetParent(null);
            back = true;
        }
    }
}

