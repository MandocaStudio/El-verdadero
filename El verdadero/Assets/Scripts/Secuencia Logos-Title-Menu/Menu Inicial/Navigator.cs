using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigationManager : MonoBehaviour
{
    public GameObject defaultSelectedButton;  
    private EventSystem eventSystem;          

    void Start()
    {
        eventSystem = EventSystem.current;  

       
        SelectDefaultButton();
    }

    
    public void SelectDefaultButton()
    {
        if (eventSystem != null && defaultSelectedButton != null)
        {
            eventSystem.SetSelectedGameObject(null);  
            eventSystem.SetSelectedGameObject(defaultSelectedButton);  
        }
    }

   
    private void OnEnable()
    {
        SelectDefaultButton();  
    }

   
    public void SetDefaultButton(GameObject newDefaultButton)
    {
        defaultSelectedButton = newDefaultButton; 
    }
}
