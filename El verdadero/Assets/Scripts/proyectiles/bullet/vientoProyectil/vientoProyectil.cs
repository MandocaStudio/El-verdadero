using System.Collections;
using UnityEngine;

public class vientoProyectil : MonoBehaviour
{
    public float bulletDamage;

    private void Update()
    {
        if (gameObject != null)
        {
            StartCoroutine(destroyAir());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            if (other.collider.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = other.collider.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(bulletDamage);
                }
            }
        }

        if (!other.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }


    }

    IEnumerator destroyAir()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
