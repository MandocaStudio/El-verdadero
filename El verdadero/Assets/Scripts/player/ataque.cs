using UnityEngine;

public class ataque : MonoBehaviour
{
    public BoxCollider atack;

    public GameObject golpeEfecto;

    public AudioClip sonidoGolpe;

    public AudioSource golpeSource;


    public void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(30f);

                golpeEfecto.SetActive(true);

                golpeSource.PlayOneShot(sonidoGolpe);
            }
        }
    }

    public void OnTriggerExit(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            golpeEfecto.SetActive(false);

        }
    }
}
