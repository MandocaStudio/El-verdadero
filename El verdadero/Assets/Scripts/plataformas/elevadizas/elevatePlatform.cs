using UnityEngine;

public class elevatePlatform : MonoBehaviour
{
    public bool activate;

    public bool neverAgain;

    [SerializeField] Animator animator; // Cambia a Animator
    [SerializeField] string triggerName; // Nombre del trigger en el Animator

    private void Start()
    {
        neverAgain = false;
    }

    private void Update()
    {
        if (activate == true && neverAgain == false)
        {
            neverAgain = true;
            animator.SetTrigger(triggerName); // Usa SetTrigger para activar la animación
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("air") && neverAgain == false)
        {
            activate = true;

            Debug.Log("entra");
        }
    }
}
