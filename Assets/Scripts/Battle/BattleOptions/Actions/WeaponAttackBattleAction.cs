public class WeaponAttackBattleAction : BattleAction {

    protected override void Run() {
        PlayerBattleData.Equipment.Weapon.Attack(PlayerBattleData.CombatData.Targets);
    }

}
