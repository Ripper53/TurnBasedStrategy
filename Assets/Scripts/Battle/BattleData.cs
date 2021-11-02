using ArtificialIntelligence.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData : MonoBehaviour {
    public Character Character;
    public MapSystem MapSystem;
    public SpriteRenderer SpriteRenderer;
    [SerializeField]
    private int health;
    public int Health {
        get => health;
        set {
            health = value;
            if (health <= 0)
                Destroy();
        }
    }
    public Equipment Equipment;

    private readonly List<BodyPart> bodyParts = new List<BodyPart>();
    public IReadOnlyList<BodyPart> BodyParts => bodyParts;
    public void AddBodyPart(BodyPart bodyPart) {
        bodyPart.Source = this;
        bodyParts.Add(bodyPart);
    }

    public BattleMind Mind;

    public CombatData CombatData;

    [System.NonSerialized]
    public BattleSystem BattleSystem;
    [System.NonSerialized]
    public BattleUnit BattleUnit;

    public void Damage(int damage) {
        Health -= damage;
    }

    public void Heal(int heal) {
        Health += heal;
    }

    public delegate void DestroyedAction(BattleData source);
    public event DestroyedAction Destroyed;
    public void Destroy() {
        BattleSystem.RemoveUnitFromBattle(BattleUnit);
        if (Mind)
            MapSystem.RemoveCharacterFromMap(Character);
        Destroyed?.Invoke(this);
    }

}
