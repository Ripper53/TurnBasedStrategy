using System.Collections.Generic;

public class HealAbility : IAbility {
    public int Heal = 1;

    public void Execute(IEnumerable<BodyPart> targets) {
        foreach (BodyPart part in targets)
            part.Heal(Heal);
    }

    public string GetDescription() {
        return $"Heal targets by {Heal}";
    }

}
