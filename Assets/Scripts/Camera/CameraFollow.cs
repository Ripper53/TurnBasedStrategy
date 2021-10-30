using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform Transform, Target;

    public void LateUpdate() {
        Vector2 pos = Transform.position;

        pos = Target.position;

        Transform.position = new Vector3(pos.x, pos.y, Transform.position.z);
    }

}
