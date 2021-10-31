using Pooler;
using System.Collections.Generic;
using UnityEngine;

namespace ArtificialIntelligence {
    public class Mind : MonoBehaviour, IPoolable<Mind> {
        public Character Character;
        /// <summary>
        /// Set by <see cref="MindBuilder"/>
        /// </summary>
        [System.NonSerialized]
        public MindBuilder MindBuilder;

        public IPooler<Mind> Pooler { get; set; }
        public void AddToPool() => Pooler.Add(this);

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
