using System.Collections.Generic;

namespace ArtificialIntelligence.Map {
    public abstract class MapWork : IWork {
        public MapMind Mind;

        public abstract IEnumerator<IMindInstruction> Run();

    }
}
