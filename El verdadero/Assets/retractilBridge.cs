using UnityEngine;

public class retractilBridge : MonoBehaviour
{
    public bool retracting;

    public Animator retractilBridgeAnimator;


    // Update is called once per frame
    void Update()
    {
        if (retracting)
        {
            retractilBridgeAnimator.SetBool("retracting", true);
        }
        else
        {
            retractilBridgeAnimator.SetBool("retracting", false);
        }
    }
}
