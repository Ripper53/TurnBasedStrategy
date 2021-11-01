using Pooler;

namespace ArtificialIntelligence.Map {
    public class MapMind : Mind<MapWork>, IPoolable<MapMind> {
        public Character Character;
        /// <summary>
        /// Set by <see cref="MindBuilder"/>
        /// </summary>
        [System.NonSerialized]
        public MapMindBuilder MindBuilder;

        public IPooler<MapMind> Pooler { get; set; }
        protected override void AddedToPool() { }

        protected override bool AddCheck(MapWork work) {
            if (work.Mind == this) return false;
            work.Mind = this;
            return true;
        }
        protected override void Removed(MapWork work) {
            work.Mind = null;
        }

    }
}
