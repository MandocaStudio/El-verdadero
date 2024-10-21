using UnityEngine;

public class spiderApears : MonoBehaviour
{

    public GameObject vfx;
    public GameObject araña;

    private void Update()
    {
        if (araña.activeSelf == true)
        {
            vfx.SetActive(true);
        }
    }

}
