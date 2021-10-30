using System.Collections.Generic;

namespace ArtificialIntelligence {
    public abstract class MindWork {
        public Mind Mind;

        public abstract IEnumerator<IMindInstruction> Run();

    }
}
