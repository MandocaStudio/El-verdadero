using UnityEngine;

public class tela : MonoBehaviour
{

    public Rigidbody telaRb;

    public bool canAtack;

    public GameObject spider;

    public triggerTela disparadorScript;

    private void Start()
    {

        spider = GameObject.FindGameObjectWithTag("spiderTrigger");
        disparadorScript = spider.GetComponent<triggerTela>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("boss"))
        {
            if (telaRb != null)
            {
                telaRb.linearVelocity = Vector3.zero;
                telaRb.angularVelocity = Vector3.zero;
                telaRb.isKinematic = true;



                disparadorScript.canEmbist = true;


            }
        }
        else if (other.gameObject.CompareTag("boss") && telaRb.isKinematic == true)
        {
            disparadorScript.canEmbist = false;
            Destroy(gameObject);


        }

    }
}
