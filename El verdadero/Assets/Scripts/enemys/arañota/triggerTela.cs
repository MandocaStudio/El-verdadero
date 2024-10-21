using UnityEngine;

public class triggerTela : MonoBehaviour
{

    [Header("disparo de tela")]
    [SerializeField] bool isTriggering;

    [SerializeField] bool hasShot;

    public Transform telaSpawn;

    [SerializeField] private float telaSpeed = 16f;

    [SerializeField] private float atackSpeed = 7f;

    public GameObject telaPrefab;

    [Header("embestida")]

    public Rigidbody spidersotaRigidbody;

    public Transform spidersotaTransform;

    public bool canEmbist;

    // Update is called once per frame
    void Update()
    {


        if (isTriggering == true)
        {
            GameObject tela = Instantiate(telaPrefab, telaSpawn.position, telaSpawn.rotation);

            // Aplicamos la fuerza en la dirección de bullet
            Rigidbody bulletRigidbody = tela.GetComponent<Rigidbody>();



            bulletRigidbody.AddForce(telaSpawn.forward * telaSpeed, ForceMode.VelocityChange);

            isTriggering = false;


        }

        if (canEmbist == true)
        {
            Transform telaDirection = GameObject.FindGameObjectWithTag("spiderTrigger").GetComponent<Transform>();
            spiderAtack(telaDirection);
        }

    }

    private void spiderAtack(Transform telaPosition)
    {



        Vector3 distance = (telaPosition.position - spidersotaTransform.position).normalized;

        spidersotaRigidbody.AddForce(distance * atackSpeed, ForceMode.VelocityChange);

    }
}
