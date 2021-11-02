using System.Collections.Generic;
using UnityEngine;
using Pooler;

public class BattleUI : MonoBehaviour {
    public BattleSystem BattleSystem;
    public AbilityBattleActionPooler AbilityPooler;
    public Transform AbilitiesParent;
    
    public void Initialize() {
        Clear();
        Draw();
    }

    public void Clear() {
        foreach (AbilityBattleAction action in actions)
            action.AddToPool();
        actions.Clear();
    }

    private readonly List<AbilityBattleAction> actions = new List<AbilityBattleAction>();
    public void Draw() {
        foreach (IAbility ability in BattleSystem.PlayerData.BattleData.Equipment.Weapon.Abilities) {
            if (((IPooler<AbilityBattleAction>)AbilityPooler).Get(out AbilityBattleAction action)) {
                action.transform.SetParent(AbilitiesParent);
                actions.Add(action);
                action.Ability = ability;
                action.Description.text = ability.GetDescription();
                action.gameObject.SetActive(true);
            } else {
                break;
            }
        }
    }

}
