using System.Collections.Generic;
using UnityEngine;

namespace ArtificialIntelligence {
    public class Mind : MonoBehaviour {
        public Character Character;

        private readonly List<MindWork> works = new List<MindWork>();
        public void Add(MindWork work) {
            if (work.Mind == this) return;
            works.Add(work);
            work.Mind = this;
        }
        public void Remove(MindWork work) {
            if (works.Remove(work)) {
                work.Mind = null;
            }
        }

        private IEnumerator<IMindInstruction> currentWork = null;
        public void Execute() {
            if (currentWork == null || !currentWork.MoveNext()) {
                foreach (MindWork work in works) {
                    currentWork = work.Run();
                    if (currentWork.MoveNext()) return;
                }
            }
        }

    }
}
