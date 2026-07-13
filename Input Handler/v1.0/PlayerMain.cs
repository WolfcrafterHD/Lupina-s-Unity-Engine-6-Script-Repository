using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour, IInputCallbacksPlayer
{
    /***Attributes***/
    
    /***Entry***/
    void Start()
    {
        InputHandler.INSTANCE.SetPlayer(this);
    }
    
    /***Public**/
    public void OnMoveCallback(InputAction.CallbackContext context)
    {
        Debug.Log($"I move {context}");
    }

    public void OnCrouchCallback(InputAction.CallbackContext context)
    {
        Debug.Log($"I Crouch {context}");
    }

    public void OnSprintCallback(InputAction.CallbackContext context)
    {
        Debug.Log($"I Sprint {context}");
    }

    public void OnInteractCallback(InputAction.CallbackContext context)
    {
        Debug.Log($"I Interact {context}");
    }

    public void OnSlowWalkCallback(InputAction.CallbackContext context)
    {
        Debug.Log($"I Slow Walk {context}");
    }
    
    /***Private***/
}
