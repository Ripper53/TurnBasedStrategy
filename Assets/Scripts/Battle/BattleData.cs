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

    public BattleMind Mind;

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

    public void Destroy() {
        BattleSystem.RemoveUnitFromBattle(BattleUnit);
        MapSystem.RemoveCharacterFromMap(Character);
    }

}
