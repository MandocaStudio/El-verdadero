using System.Collections;
using UnityEngine;

public class culatazoPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public movimientoPlayer movimiento;

    public float downForce = 100f;

    public bool canFall = false;

    public MeshCollider culatazoZone;
    public void culatazo()
    {
        if (Input.GetButtonDown("Attack") && movimiento.rojo == true && canFall == true)
        {


            StartCoroutine(culazo());
        }

    }

    IEnumerator culazo()
    {
        movimiento.rb.isKinematic = true;
        movimiento.canMove = false;

        culatazoZone.enabled = true;

        yield return new WaitForSeconds(0.5f);

        movimiento.rb.isKinematic = false;

        movimiento.rb.AddForce(Vector3.down * downForce, ForceMode.Impulse);


        yield return new WaitForSeconds(0.5f);
        movimiento.canMove = true;
        culatazoZone.enabled = false;
    }
}
