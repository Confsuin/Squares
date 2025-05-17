using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class GameBehaviour : MonoBehaviour
{
    public bool IsAEscapeMenuActive = false;
    public bool IsADebugMenuActive = false;
    void Start()
    {
        GetReferences();
    }
    private void GetReferences()
    {

    }
}
