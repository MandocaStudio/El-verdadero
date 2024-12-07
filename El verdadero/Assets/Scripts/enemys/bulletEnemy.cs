using System.Collections;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] public float Damage;

    [SerializeField] public float life;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<health>().takeDamage(Damage);

            Destroy(gameObject);
        }
        else if (other.CompareTag("attackEnemyContainer"))
        {
            Destroy(gameObject);

        }


    }

    private void Start()
    {
        StartCoroutine(bulletLife(life));
    }
    IEnumerator bulletLife(float life)
    {
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
}
