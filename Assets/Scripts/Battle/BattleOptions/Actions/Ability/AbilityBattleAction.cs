using Pooler;
using TMPro;

public class AbilityBattleAction : BattleAction, IPoolable<AbilityBattleAction> {
    public IAbility Ability;
    public TextMeshProUGUI Description;

    public IPooler<AbilityBattleAction> Pooler { get; set; }
    public void AddToPool() {
        Pooler.Add(this);
        gameObject.SetActive(false);
    }

    protected override void Run() {
        Ability.Execute(PlayerBattleData.CombatData.Targets);
    }

}
