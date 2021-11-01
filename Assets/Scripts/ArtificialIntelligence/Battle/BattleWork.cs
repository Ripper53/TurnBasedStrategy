using System.Collections.Generic;

namespace ArtificialIntelligence.Battle {
    public abstract class BattleWork : IWork {
        public BattleMind Mind;

        public abstract IEnumerator<IMindInstruction> Run();

    }
}
