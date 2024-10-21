using UnityEngine;
using System.Collections;
public class PlayerHealing : MonoBehaviour
{
    public GameObject healingVFX; 
        public KeyCode healingKey = KeyCode.R;  

    void Update()
    {
        if (Input.GetKeyDown(healingKey))
        {
            ActivateHealingVFX();
        }
    }

    void ActivateHealingVFX()
    {
        if (healingVFX != null)
        {
            healingVFX.SetActive(true);
            StartCoroutine(DeactivateVFXAfterTime(2f)); 
        }
    }

    IEnumerator DeactivateVFXAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        healingVFX.SetActive(false);  
    }
}
