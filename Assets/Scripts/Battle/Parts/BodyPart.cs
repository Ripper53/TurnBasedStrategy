using UnityEngine;

public class BodyPart {
    public BattleData Source { get; internal set; }
    public readonly string Name;
    public readonly Vector2 Position, Size;

    public BodyPart(string name, Vector2 position, Vector2 size) {
        Name = name;
        Position = position;
        Size = size;
    }

    public void Damage(int damage) {
        Source.Damage(damage);
    }

    public void Heal(int heal) {
        Source.Heal(heal);
    }

}
