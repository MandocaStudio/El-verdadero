using UnityEngine;

public class trigger : MonoBehaviour
{
    [Header("charge variables")]
    public bool canShoting = true;


    [Header("Shoot variables")]
    public float shotCooldown;
    public float bulletSpeed = 45f;
    public float bulletDamage = 30f;


    public bool shootingBool;

    [Header("objects variables")]
    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    public movimientoPlayer player;

    public menuRadialController color;

    public GameObject arrow;


    private void Update()
    {
        if (color.colorActivated == 1f && canShoting == true)
        {
            if (Input.GetButtonDown("Hability"))
            {
                arrow.SetActive(true);

            }
            if (Input.GetButtonUp("Hability"))
            {
                shooting();
                arrow.SetActive(false);

            }
            else if (Input.GetAxis("RightTrigger") >= 0.5f && canShoting == true && shotCooldown == 0)
            {
                arrow.SetActive(true);
                shootingBool = true;


            }
            if (Input.GetAxis("RightTrigger") < 0.5f && shootingBool == true)
            {
                shooting();
                shootingBool = false;
                arrow.SetActive(false);


            }

        }

        if (canShoting == false)
        {
            shotCooldown += Time.deltaTime;
            if (shotCooldown >= 3)
            {
                canShoting = true;
                shotCooldown = 0;

            }
        }


        if (color.colorActivated != 1f && canShoting == false)
        {
            shotCooldown += Time.deltaTime;
            if (shotCooldown >= 3)
            {
                canShoting = true;
                shotCooldown = 0;

            }

        }

    }

    public void shooting()
    {



        // Instanciamos la bala (bulletPrefab) en la posición y rotación de bulletSpawn
        GameObject bulletUsed = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Aplicamos la fuerza en la dirección de bullet
        Rigidbody bulletRigidbody = bulletUsed.GetComponent<Rigidbody>();
        vientoProyectil bulletScript = bulletUsed.GetComponent<vientoProyectil>();
        bulletRigidbody.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);

        // Evitamos que se dispare nuevamente hasta que se cumpla la condición de cooldown
        canShoting = false;

        // Activamos el menú radial
        color.canUseRadialMenu = true;

        // Asignamos el daño a la bala
        bulletScript.bulletDamage = bulletDamage;


    }
}
