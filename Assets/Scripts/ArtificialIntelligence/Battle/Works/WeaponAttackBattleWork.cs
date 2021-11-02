using System.Collections.Generic;

namespace ArtificialIntelligence.Battle.Works {
    public class WeaponAttackBattleWork : BattleWork {

        public override IEnumerator<IMindInstruction> Run() {
            Mind.Data.Equipment.Weapon.Attack(Mind.Data.CombatData.Targets);
            yield break;
        }

    }
}
