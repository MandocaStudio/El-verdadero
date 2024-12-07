using System.Collections;
using UnityEngine;

public class triggerEnemyProyectil : MonoBehaviour
{
    [Header("charge variables")]
    public bool canShoting = true;

    public EnemyMovement enemy;

    [Header("Shoot variables")]
    [SerializeField] private float shotCooldown;
    public float bulletSpeed;
    public float bulletDamage;

    public bool shoot;

    [SerializeField] private float BulletLife;



    [Header("objects variables")]
    public GameObject bulletPrefab;

    public Transform bulletSpawn;



    private void Update()
    {

        if (canShoting && enemy.animator.GetBool("attack"))
        {

            shooting();

        }
    }


    public void shooting()
    {
        shoot = false;
        // Instanciamos la bala (bulletPrefab) en la posición y rotación de bulletSpawn
        GameObject bulletUsed = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Aplicamos la fuerza en la dirección de bullet
        Rigidbody bulletRigidbody = bulletUsed.GetComponent<Rigidbody>();
        BulletEnemy bulletScript = bulletUsed.GetComponent<BulletEnemy>();
        bulletRigidbody.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);

        // Evitamos que se dispare nuevamente hasta que se cumpla la condición de cooldown
        canShoting = false;
        // Asignamos el daño a la bala
        bulletScript.Damage = bulletDamage;
        bulletScript.life = BulletLife;

        StartCoroutine(shootCooldown());
    }

    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(shotCooldown);
        canShoting = true;
    }
}
