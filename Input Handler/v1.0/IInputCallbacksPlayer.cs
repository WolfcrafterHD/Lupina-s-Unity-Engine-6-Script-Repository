/*
 * Lupinus
 * 06.07.2026
 * This Interface simply holds Callback Methods for each of the Methods we are going to give to the PlayerInput Component.
 * This one is made for the Inputs for the Player Inputs to current Player Models.
 */
using UnityEngine.InputSystem;
public interface IInputCallbacksPlayer
{
    public void OnMoveCallback(InputAction.CallbackContext context);
    public void OnCrouchCallback(InputAction.CallbackContext context);
    public void OnSprintCallback(InputAction.CallbackContext context);
    public void OnInteractCallback(InputAction.CallbackContext context);
    
    public void OnSlowWalkCallback(InputAction.CallbackContext context);
}
