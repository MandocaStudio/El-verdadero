using UnityEngine;

public class live : MonoBehaviour
{
    public float bulletDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);

            if (other.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = other.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(bulletDamage);
                }
            }
        }


    }
}
