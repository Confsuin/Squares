using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventSystemAccess : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    public Selectable firstItemToSelect;
    private void Start()
    {
        if (eventSystem != null)
            return;

        eventSystem.firstSelectedGameObject = firstItemToSelect.gameObject;
    }
}
