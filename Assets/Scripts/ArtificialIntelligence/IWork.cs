using System.Collections.Generic;

namespace ArtificialIntelligence {
    public interface IWork {
        public IEnumerator<IMindInstruction> Run();
    }
}
