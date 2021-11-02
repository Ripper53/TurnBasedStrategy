using System.Collections.Generic;
using UnityEngine;
using Pooler;

public class BattleUI : MonoBehaviour {
    public BattleSystem BattleSystem;
    public AbilityBattleActionPooler AbilityPooler;
    public Transform AbilitiesParent;

    public BodyPartUIPooler BodyPartUIPooler;

    private readonly List<IPoolable> poolables = new List<IPoolable>();
    public void Initialize() {
        Clear();
        Draw();

        foreach (BattleUnit unit in BattleSystem.UnitsCommencedInBattle) {
            foreach (BodyPart part in unit.Data.BodyParts) {
                if (((IPooler<BodyPartUI>)BodyPartUIPooler).Get(out BodyPartUI bodyPartUI)) {
                    poolables.Add(bodyPartUI);
                    bodyPartUI.BattleUnit = unit;
                    bodyPartUI.BodyPart = part;
                    bodyPartUI.transform.SetParent(unit.UIParent);
                    bodyPartUI.transform.localPosition = part.Position;
                    bodyPartUI.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Clear() {
        foreach (AbilityBattleAction action in poolables)
            action.AddToPool();
        poolables.Clear();
    }

    public void Draw() {
        foreach (IAbility ability in BattleSystem.PlayerData.BattleData.Equipment.Weapon.Abilities) {
            if (((IPooler<AbilityBattleAction>)AbilityPooler).Get(out AbilityBattleAction action)) {
                action.transform.SetParent(AbilitiesParent);
                poolables.Add(action);
                action.Ability = ability;
                action.Description.text = ability.GetDescription();
                action.gameObject.SetActive(true);
            } else {
                break;
            }
        }
    }

}
