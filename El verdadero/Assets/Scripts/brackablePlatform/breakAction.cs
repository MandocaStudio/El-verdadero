using System.Collections;
using UnityEngine;

public class breakAction : MonoBehaviour
{

    public GameObject platform;

    public bool romper = true;



    public float actionTimmer;


    private void Update()
    {
        if (actionTimmer <= 2.5f)
        {
            actionTimmer += Time.deltaTime;
        }


        if (romper == false && actionTimmer >= 2.5f)
        {
            actionTimmer = 0;
            platform.SetActive(romper);
            romper = true;

        }
        else if (romper == true && actionTimmer >= 1.99f)
        {
            actionTimmer = 0;
            platform.SetActive(romper);
            romper = false;
        }
    }


}
