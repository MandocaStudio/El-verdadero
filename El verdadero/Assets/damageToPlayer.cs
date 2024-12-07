using UnityEngine;

public class damageToPlayer : MonoBehaviour
{

    [SerializeField] private float Damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<health>().takeDamage(Damage);
        }
    }
}
