using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FocusCanvas : MonoBehaviour
{
    public GameObject firstUIElement; // The first UI element to focus on (e.g., a button)

    void Awake()
    {
        EnsureEventSystemExists();
    }

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

    private void EnsureEventSystemExists()
    {
        // Check if an EventSystem already exists in the scene
        if (FindObjectOfType<EventSystem>() == null)
        {
            // Create a new EventSystem GameObject
            GameObject eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>(); // Add this if you are using standalone input
            // Optionally add other input modules depending on your project requirements
        }
    }
}
