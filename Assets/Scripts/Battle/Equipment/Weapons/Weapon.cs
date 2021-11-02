using System.Collections.Generic;

public class Weapon {
    public int Damage;

    private readonly List<BodyPart> copyOfTargets = new List<BodyPart>();
    public void Attack(IEnumerable<BodyPart> targets) {
        copyOfTargets.Clear();
        copyOfTargets.AddRange(targets);
        foreach (BodyPart target in copyOfTargets) {
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
