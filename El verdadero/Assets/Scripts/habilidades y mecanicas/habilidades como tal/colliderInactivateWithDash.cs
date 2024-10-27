using UnityEngine;

public class ColliderInactivateWithDash : MonoBehaviour
{
    public Collider colliderObject;
    [SerializeField] private movimientoPlayer player;

    [SerializeField] private Transform playerTransform;

    public float activationDistance;

    private void Update()
    {
        if (player == null && Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) <= activationDistance)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            player = GameObject.FindWithTag("Player").GetComponent<movimientoPlayer>();
        }

        else if (player != null && Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) > activationDistance)
        {
            playerTransform = null;
            player = null;
        }

        if (player != null && Vector3.Distance(transform.position, playerTransform.transform.position) <= activationDistance)
        {
            if (player.dash.isDashing == true)
            {
                colliderObject.enabled = false;

            }
            else
            {
                colliderObject.enabled = true;

            }
        }
    }


}
