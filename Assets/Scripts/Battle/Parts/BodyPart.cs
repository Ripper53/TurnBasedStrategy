using UnityEngine;

public class BodyPart {
    public BattleData Source { get; internal set; }
    public readonly string Name;
    public readonly Vector2 Position;

    public BodyPart(string name, Vector2 position) {
        Name = name;
        Position = position;
    }

    public void Damage(int damage) {
        Source.Damage(damage);
    }

    public void Heal(int heal) {
        Source.Heal(heal);
    }

}
