using System.Collections.Generic;

namespace Pooler {
    public interface IStackPooler<T> : IPooler<T> where T : IPoolable<T> {
        protected Stack<T> CreatedPrefabsStack { get; }
        IEnumerable<T> IPooler<T>.CreatedPrefabs => CreatedPrefabsStack;
        public int Count { get; protected set; }
        bool IPooler<T>.Get(out T obj) {
            if (CreatedPrefabsStack.TryPop(out obj)) {
                return true;
            } else if (Count < Max) {
                Count += 1;
                obj = Create();
                obj.Pooler = this;
                return true;
            }
            return false;
        }
        void IPooler<T>.Add(T obj) => CreatedPrefabsStack.Push(obj);
    }
}
