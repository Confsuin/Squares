using UnityEngine;
using UnityEngine.EventSystems;

public class SetAsSelected : MonoBehaviour
{
    public void SetButtonAsSelected()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
