using System.Collections.Generic;
using UnityEngine;

public class BattleOptions : MonoBehaviour {
    public BattleSystem BattleSystem;

    private readonly List<BattleUnit> units = new List<BattleUnit>();
    public void PlayerMadeMove() {
        units.Clear();
        units.AddRange(BattleSystem.UnitsCommencedInBattle);
        foreach (BattleUnit unit in units) {
            if (unit.Data.Mind)
                unit.Data.Mind.Execute();
        }
    }

}
