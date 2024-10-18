using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FocusCanvas : MonoBehaviour
{
    public GameObject firstUIElement; // The first UI element to focus on (e.g., a button)

    void Start()
    {
        SetFocusOnUIElement();
    }

    void Update()
    {
        // Check if any GameObject is currently selected
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            // Reassign focus to the first UI element
            SetFocusOnUIElement();
        }
    }

    public void SetFocusOnUIElement()
    {
        // Ensure the EventSystem is selecting the desired UI element
        EventSystem.current.SetSelectedGameObject(null); // Clear the current selection
        EventSystem.current.SetSelectedGameObject(firstUIElement);
    }
}

