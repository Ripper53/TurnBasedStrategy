using UnityEngine;

public abstract class PlayerInput : MonoBehaviour {
    private PlayerControls controls;

    protected void Awake() {
        controls = new PlayerControls();
        AddListeners(controls);
    }

    protected void OnEnable() {
        controls.Enable();
    }

    protected void OnDisable() {
        controls.Disable();
    }

    protected void OnDestroy() {
        controls.Dispose();
    }

    protected abstract void AddListeners(PlayerControls controls);

}
