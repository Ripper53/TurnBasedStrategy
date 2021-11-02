using UnityEngine;

public abstract class BattleAction : MonoBehaviour {
    public BattleOptions BattleOptions;
    public BattleData PlayerBattleData => BattleOptions.BattleSystem.PlayerData.BattleData;

    public void Execute() {
        Run();
        BattleOptions.PlayerMadeMove();
    }
    protected abstract void Run();

}
