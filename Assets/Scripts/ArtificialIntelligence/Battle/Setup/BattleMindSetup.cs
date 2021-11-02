using ArtificialIntelligence.Battle;
using UnityEngine;

public abstract class BattleMindSetup : MonoBehaviour {
    public BattleMind BattleMind;

    protected void Awake() {
        SetupBattleMind(BattleMind);
    }

    protected abstract void SetupBattleMind(BattleMind battleMind);

}
