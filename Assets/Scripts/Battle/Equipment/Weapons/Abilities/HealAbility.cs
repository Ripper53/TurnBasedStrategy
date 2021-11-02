using System.Collections.Generic;

public class HealAbility : IAbility {
    public int Heal = 1;

    public void Execute(IEnumerable<BattleData> targets) {
        foreach (BattleData battleData in targets)
            battleData.Heal(Heal);
    }

    public string GetDescription() {
        return $"Heal targets by {Heal}";
    }

}
