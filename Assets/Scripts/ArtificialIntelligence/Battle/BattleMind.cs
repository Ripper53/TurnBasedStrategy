using Pooler;

namespace ArtificialIntelligence.Battle {
    public class BattleMind : Mind<BattleWork>, IPoolable<BattleMind> {
        public BattleData Data;

        [System.NonSerialized]
        public BattleData Target;

        public IPooler<BattleMind> Pooler { get; set; }
        protected override void AddedToPool() { }

        protected override bool AddCheck(BattleWork work) {
            if (work.Mind == this) return false;
            work.Mind = this;
            return true;
        }
        protected override void Removed(BattleWork work) {
            work.Mind = null;
        }

    }
}
