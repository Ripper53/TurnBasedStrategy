using ArtificialIntelligence;
using UnityEngine;

public class PlayerCharacterInput : PlayerInput {
    public PlayerCharacter PlayerCharacter;
    public Character Character;
    public MindBuilder MindBuilder;

    protected override void AddListeners(PlayerControls controls) {
        controls.Movement.Horizontal.performed += Horizontal_performed;
        controls.Movement.Vertical.performed += Vertical_performed;
    }

    private void Horizontal_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (!PlayerCharacter.IsTurn) return;
        if (obj.ReadValue<float>() > 0f) {
            // Right
            if (Character.MoveRight())
                FinishTurn();
        } else {
            // Left
            if (Character.MoveLeft())
                FinishTurn();
        }
    }

    private void Vertical_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (!PlayerCharacter.IsTurn) return;
        if (obj.ReadValue<float>() > 0f) {
            // Up
            if (Character.MoveUp())
                FinishTurn();
        } else {
            // Down
            if (Character.MoveDown())
                FinishTurn();
        }
    }

    private void FinishTurn() {
        PlayerCharacter.IsTurn = false;
        if (MindBuilder.GetOccupation(Character.Position, out Mind mind)) {
            Debug.Log("Mind: " + mind.gameObject.name, mind);
        }
        PlayerCharacter.FinishTurn();
    }

}
