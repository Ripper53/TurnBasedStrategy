using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the data relating to combat.
/// </summary>
public class CombatData : MonoBehaviour {

    private readonly Dictionary<BattleData, HashSet<BodyPart>> targets = new Dictionary<BattleData, HashSet<BodyPart>>();
    private readonly List<BodyPart> allBodyParts = new List<BodyPart>();
    public IReadOnlyCollection<BodyPart> Targets => allBodyParts;
    public void AddTarget(BodyPart target) {
        if (targets.TryGetValue(target.Source, out HashSet<BodyPart> parts)) {
            if (parts.Add(target))
                allBodyParts.Add(target);
        } else {
            targets.Add(target.Source, new HashSet<BodyPart> { target });
            target.Source.Destroyed += Target_Destroyed;
            allBodyParts.Add(target);
        }
    }

    public void RemoveTarget(BodyPart target) {
        if (targets.TryGetValue(target.Source, out HashSet<BodyPart> parts)) {
            if (parts.Remove(target) && parts.Count == 0)
                targets.Remove(target.Source);
            target.Source.Destroyed -= Target_Destroyed;
            allBodyParts.Remove(target);
        }
    }

    private void Target_Destroyed(BattleData source) {
        targets.Remove(source);
        source.Destroyed -= Target_Destroyed;
    }

    public void Initialize() {
        targets.Clear();
    }

}
