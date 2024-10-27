using Unity.VisualScripting;
using UnityEngine;

public class ShadowCaster : MonoBehaviour
{
    public GameObject shadowPrefab; // El prefab de la "sombra" que se va a proyectar
    private GameObject shadowInstance;

    [SerializeField] private Transform positionShadow;

    public movimientoPlayer player;

    void Start()
    {
        // Instancia la sombra al principio
        shadowInstance = Instantiate(shadowPrefab, transform.position, Quaternion.identity);

        positionShadow = shadowInstance.transform;
    }

    void Update()
    {

        positionShadow.position = new Vector3(player.x, positionShadow.position.y, player.z);

        // Genera un raycast desde el objeto hacia abajo
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        // Define la capa a ignorar
        int layerMask = LayerMask.GetMask("ignoreRaycast");
        // Si el raycast golpea cualquier objeto (sin depender de capas específicas)
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
        {


            // Mueve la "sombra" a la posición donde golpea el raycast
            shadowInstance.transform.position = hit.point;


            float distance = Vector3.Distance(transform.position, hit.point);
            shadowInstance.transform.localScale = Vector3.one * (1 + distance * 0.1f);


        }


    }
}
