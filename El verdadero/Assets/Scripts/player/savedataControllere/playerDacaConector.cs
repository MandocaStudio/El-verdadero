using UnityEngine;

public class playerDataConector : MonoBehaviour
{
    [Header("Conección con Save Manager")]
    public SaveLoadManager savePlayerData;

    [Header("Player")]
    public movimientoPlayer player;

    [Header("Animator de Guardado")]
    public Animator saveAnimator;
    private void Start()
    {
        //guardar datos al inicio del juego
        // PlayerData initialData = new PlayerData();
        // initialData.health = 2f;

        // initialData.maxHealth = player.healthPlayer.maxHealthPlayer;
        // // initialData.playerBasicDamage;
        // initialData.playerPosition = player.transform.position;
        // //Colores Activos
        // initialData.azul = player.azul;
        // initialData.rojo = player.rojo;
        // initialData.amarillo = player.amarillo;
        // initialData.magenta = player.magenta;
        // initialData.cyan = player.cyan;
        // initialData.verde = player.verde;
        // savePlayerData.SavePlayerData(initialData);


        // Cargar datos al inicio del juego
        PlayerData data = savePlayerData.LoadPlayerData();

        if (data != null)
        {
            player.healthPlayer.healthPlayer = data.health;
            player.transform.position = data.playerPosition;
            player.amarillo = data.amarillo;
            player.azul = data.azul;
            player.verde = data.verde;
            player.cyan = data.cyan;
            player.rojo = data.rojo;
            player.magenta = data.magenta;
            Debug.Log("Loaded Health: " + data.health + data.playerPosition);
        }
    }

    private void Update()
    {
        // if (Input.GetButtonDown("Interact"))
        // {
        //     PlayerData initialData = new PlayerData();
        //     initialData.health = player.healthPlayer.healthPlayer;

        //     initialData.maxHealth = player.healthPlayer.maxHealthPlayer;
        //     // initialData.playerBasicDamage;
        //     initialData.playerPosition = player.transform.position;
        //     //Colores Activos
        //     initialData.azul = player.azul;
        //     initialData.rojo = player.rojo;
        //     initialData.amarillo = player.amarillo;
        //     initialData.magenta = player.magenta;
        //     initialData.cyan = player.cyan;
        //     initialData.verde = player.verde;
        //     savePlayerData.SavePlayerData(initialData);

        //     saveAnimator.SetBool("isSaving", true);
        // }
        // if (saveAnimator.GetBool("isSaving"))
        // {
        //     AnimatorStateInfo stateInfo = saveAnimator.GetCurrentAnimatorStateInfo(0);

        //     if (stateInfo.normalizedTime >= 0.9f && stateInfo.IsName("saving"))
        //     {
        //         saveAnimator.SetBool("isSaving", false);
        //     }
        // }
    }
}