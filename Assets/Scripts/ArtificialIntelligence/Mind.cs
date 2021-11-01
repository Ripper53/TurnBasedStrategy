using Pooler;
using System.Collections.Generic;

namespace ArtificialIntelligence {
    public abstract class Mind<T> : ObjectPoolable where T : IWork {

        private readonly List<T> works = new List<T>();
        public void Add(T work) {
            if (AddCheck(work))
                works.Add(work);
        }
        protected abstract bool AddCheck(T work);
        public void Remove(T work) {
            if (works.Remove(work))
                Removed(work);
        }
        protected abstract void Removed(T work);

        private IEnumerator<IMindInstruction> currentWork = null;

        public void Execute() {
            if (currentWork == null || !currentWork.MoveNext()) {
                foreach (T work in works) {
                    currentWork = work.Run();
                    if (currentWork.MoveNext()) return;
                }
            }
        }

    }
}
