using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the data relating to combat.
/// </summary>
public class CombatData : MonoBehaviour {

    private readonly List<BattleData> targets = new List<BattleData>();
    public IReadOnlyList<BattleData> Targets => targets;
    public void AddTarget(BattleData target) {
        targets.Add(target);
        target.Destroyed += Target_Destroyed;
    }

    public void RemoveTarget(BattleData target) {
        if (targets.Remove(target))
            target.Destroyed -= Target_Destroyed;
    }

    private void Target_Destroyed(BattleData source) {
        targets.Remove(source);
        source.Destroyed -= Target_Destroyed;
    }

    public void Initialize() {
        targets.Clear();
    }

}
