using UnityEngine;

public class tinta : MonoBehaviour
{

    public BoxCollider box;

    public float lifeTime = 8f;

    private void Update()
    {
        Destroy(gameObject, lifeTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("me quemo!!!");
        }
    }


}
