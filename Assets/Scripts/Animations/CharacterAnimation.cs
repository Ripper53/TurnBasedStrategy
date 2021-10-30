using UnityEngine;

public class CharacterAnimation : MonoBehaviour {
    public Character Character;
    public Transform Transform;
    public float SmoothTime = 0.1f;

    private Vector2 velocity;

    protected void OnEnable() {
        velocity = Vector2.zero;
    }

    protected void Update() {
        Transform.position = Vector2.SmoothDamp(Transform.position, Character.Position, ref velocity, SmoothTime);
    }

}
