using UnityEngine;

public class MapEditorCamera : PlayerInput {
    public Transform Transform;
    public float Speed = 1f;

    protected override void AddListeners(PlayerControls controls) {
        controls.Pointer.Position.performed += Position_performed;  
    }

    private Vector2 position;
    private void Position_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        position = obj.ReadValue<Vector2>();
    }

    protected void LateUpdate() {
        const float r = 5f;
        if (position.x < r) {
            // Move Left
            Transform.position -= new Vector3(Speed * Time.deltaTime, 0f);
        } else if (position.x > Screen.width - r) {
            // Move Right
            Transform.position += new Vector3(Speed * Time.deltaTime, 0f);
        }
        if (position.y < r) {
            // Move Down
            Transform.position -= new Vector3(0f, Speed * Time.deltaTime);
        } else if (position.y > Screen.height - r) {
            // Move Up
            Transform.position += new Vector3(0f, Speed * Time.deltaTime);
        }
    }

}
