/*
 * Lupinus
 * 12.07.2026
 * This is the Nexus from where all the Callbacks are getting sent.
 */

using SaintsField;
using SaintsField.Playa;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    /***Attributes***/
    public static InputHandler INSTANCE {private set; get;};
    
    [ShowInInspector, ReadOnly]
    private PlayerMain _currentPlayer; public void SetPlayer(PlayerMain pPlayer) {  _currentPlayer = pPlayer; }
    
    /***Entry***/
    void Awake()
    {
        if (INSTANCE is not null && INSTANCE != this) Destroy(this.gameObject);
        else INSTANCE = this;
    }
    
    /***Public***/
    public void OnMove(InputAction.CallbackContext context)
    {
        _currentPlayer?.OnMoveCallback(context);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        _currentPlayer?.OnCrouchCallback(context);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        _currentPlayer?.OnSprintCallback(context);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _currentPlayer?.OnInteractCallback(context);
    }

    public void OnSlowWalk(InputAction.CallbackContext context)
    {
        _currentPlayer?.OnSlowWalkCallback(context);
    }
    
    /***Private***/

}
