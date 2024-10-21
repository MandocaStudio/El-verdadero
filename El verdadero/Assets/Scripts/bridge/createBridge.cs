using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class createBridge : MonoBehaviour
{
    public GameObject bridge;
    public CinemachineCamera cinemachineCamera;
    public Animation bridgeAnimator;
    public bool canInteract;

    public Animator animatorCrystal;
    public Light lightControler;
    public Renderer sphere;

    public string activate = "#FAAD00";
    public string desactivate = "#FA00ED";

    public movimientoPlayer player;
    public jumpPlayer playerJump;
    public atackGestion playerAttack;
    public pasos pasosPlayer;

    public bool neverAgain;
    public dialogoGeneral dialogos;

    private void Start()
    {
        canInteract = true;
    }

    private void Update()
    {
        // Solo permite la interacción si canInteract es true, nunca se ha activado y si tiene todos los componentes
        if (Input.GetButtonDown("Interact") && canInteract && !neverAgain && HasRequiredComponents())
        {
            canInteract = false;
            StartCoroutine(dialogos.finalizar());
            StartCoroutine(createBridgeActivate());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogos != null)
            {
                canInteract = false;
                StartCoroutine(dialogos.finalizar());

            }

            Rigidbody playerRB = other.GetComponent<Rigidbody>();
            playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<movimientoPlayer>();
            playerJump = other.GetComponent<jumpPlayer>();
            playerAttack = other.GetComponent<atackGestion>();
            pasosPlayer = other.GetComponent<pasos>();

            if (HasRequiredComponents())
            {
                dialogos.managerObject = GameObject.Find("Dialogos");
                Transform childTransform = dialogos.managerObject.transform.Find("Manager Secuencia");
                dialogos.managerSecuencia = childTransform.GetComponent<DialogueSequenceManager>();
                StartCoroutine(dialogos.empezar());
                canInteract = true;
            }
            else
            {
                // Si falta algún componente, desactiva la interacción
                canInteract = false;
            }
        }
    }

    // Método que valida si todos los componentes requeridos están presentes
    private bool HasRequiredComponents()
    {
        return player != null && playerJump != null && playerAttack != null && pasosPlayer != null && dialogos != null;
    }

    IEnumerator createBridgeActivate()
    {
        animatorCrystal.SetBool("activating", true);
        Color newColor;

        player.enabled = false;
        playerJump.enabled = false;
        playerAttack.enabled = false;
        pasosPlayer.enabled = false;

        cinemachineCamera.Priority = 2;

        yield return new WaitForSeconds(2f);

        bridgeAnimator.Play("createBridge");

        if (bridge.activeSelf)
        {
            if (ColorUtility.TryParseHtmlString(desactivate, out newColor))
            {
                sphere.material.color = newColor;
                lightControler.color = newColor;
            }
        }
        else
        {
            if (ColorUtility.TryParseHtmlString(activate, out newColor))
            {
                sphere.material.color = newColor;
                lightControler.color = newColor;
            }
        }

        yield return new WaitForSeconds(2f);
        cinemachineCamera.Priority = 0;
        player.enabled = true;
        playerJump.enabled = true;
        playerAttack.enabled = true;
        pasosPlayer.enabled = true;

        animatorCrystal.SetBool("activating", false);
        neverAgain = true;
        dialogos.enabled = false;
    }
}
