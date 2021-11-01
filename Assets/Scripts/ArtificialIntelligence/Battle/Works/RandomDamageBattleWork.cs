using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArtificialIntelligence.Battle.Works {
    public class RandomDamageBattleWork : BattleWork {

        public override IEnumerator<IMindInstruction> Run() {
            Mind.Target.Damage(1);
            yield break;
        }

    }
}
