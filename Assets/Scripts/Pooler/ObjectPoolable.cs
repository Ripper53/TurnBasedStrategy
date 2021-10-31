using UnityEngine;

namespace Pooler {
    public abstract class ObjectPoolable : MonoBehaviour, IPoolable {
        public Transform PoolerHolder;

        public void AddToPool() {
            gameObject.SetActive(false);
            transform.SetParent(PoolerHolder);
            AddedToPool();
        }

        protected abstract void AddedToPool();

    }
}
