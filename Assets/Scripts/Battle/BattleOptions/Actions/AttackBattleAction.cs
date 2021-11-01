using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBattleAction : BattleAction {
    public PlayerBattleField BattleField;

    protected override void Run() {
        BattleField.Target.Damage(1);
    }

}
