using UnityEngine;

public class ataqueCulatazo : MonoBehaviour
{
    public MeshCollider atack;




    public void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(30f);
            }
        }
    }
}
