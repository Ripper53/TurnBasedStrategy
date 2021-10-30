using UnityEngine;

public class PlayerCharacterInput : PlayerInput {
    public PlayerCharacter PlayerCharacter;
    public Character Character;

    protected override void AddListeners(PlayerControls controls) {
        controls.Movement.Horizontal.performed += Horizontal_performed;
        controls.Movement.Vertical.performed += Vertical_performed;
    }

    private void Horizontal_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (obj.ReadValue<float>() > 0f) {
            // Right
            Character.MoveBy(new Vector2Int(1, 0));
        } else {
            // Left
            Character.MoveBy(new Vector2Int(-1, 0));
        }
        PlayerCharacter.FinishTurn();
    }

    private void Vertical_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (obj.ReadValue<float>() > 0f) {
            // Up
            Character.MoveBy(new Vector2Int(0, 1));
        } else {
            // Down
            Character.MoveBy(new Vector2Int(0, -1));
        }
        PlayerCharacter.FinishTurn();
    }

}
