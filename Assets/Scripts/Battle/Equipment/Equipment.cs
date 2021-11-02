using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    private readonly Weapon bareWeapon = new Weapon {
        Damage = 1
    };
    public Weapon Weapon { get; private set; }

    protected void Awake() {
        for (int i = 0; i < 10; i++)
            bareWeapon.AddAbility(new HealAbility());
        Weapon = bareWeapon;
    }

    public void EquipWeapon(Weapon weapon) {
        if (weapon == null) {
            Weapon = new Weapon();
            return;
        }
        Weapon = weapon;
    }

}
