using UnityEngine;

public abstract class BodyPartsSetup : MonoBehaviour {
    public BattleData BattleData;

    protected void Awake() {
        SetupBodyParts(BattleData);
    }

    protected abstract void SetupBodyParts(BattleData battleData);

}
