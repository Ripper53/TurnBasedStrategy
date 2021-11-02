using System.Collections.Generic;

public class Weapon {
    public int Damage;

    private readonly List<BattleData> targets = new List<BattleData>();
    public void Attack(IEnumerable<BattleData> targets) {
        this.targets.Clear();
        this.targets.AddRange(targets);
        foreach (BattleData target in this.targets) {
            target.Damage(Damage);
        }
    }

    private readonly List<IAbility> abilities = new List<IAbility>();
    public IReadOnlyList<IAbility> Abilities => abilities;

    public void AddAbility(IAbility ability) {
        abilities.Add(ability);
    }
    public void RemoveAbility(IAbility ability) {
        abilities.Remove(ability);
    }

}
