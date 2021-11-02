using ArtificialIntelligence.Battle;
using ArtificialIntelligence.Battle.Works;

public class SpookyBattleMindSetup : BattleMindSetup {

    protected override void SetupBattleMind(BattleMind battleMind) {
        battleMind.Add(new WeaponAttackBattleWork());
    }

}
