using UnityEngine;

public class BattleOptions : MonoBehaviour {
    public BattleSystem BattleSystem;
    
    public void PlayerMadeMove() {
        foreach (BattleUnit unit in BattleSystem.UnitsCommencedInBattle) {
            if (unit.Data.Mind)
                unit.Data.Mind.Execute();
        }
    }

}
