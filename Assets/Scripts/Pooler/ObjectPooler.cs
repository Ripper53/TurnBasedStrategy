using System.Collections.Generic;
using UnityEngine;

namespace Pooler {
    public interface IPooler<T> where T : IPoolable<T> {
        public T Prefab { get; }
        public IEnumerable<T> CreatedPrefabs { get; }
        public int Max { get; }
        public bool Get(out T obj);
        /// <summary>
        /// Creates a new object from the <see cref="Prefab"/>.
        /// </summary>
        public T Create();
        public void Add(T obj);
    }
    public interface IMonoPooler<T> : IPooler<T> where T : Component, IPoolable<T> {
        T IPooler<T>.Create() => Object.Instantiate(Prefab);
    }
    [System.Serializable]
    public class Pooler<T> : IStackPooler<T>, IMonoPooler<T> where T : Component, IPoolable<T> {
        public T Prefab { get; set; }
        public int Max { get; set; }
        private int count = 0;
        public int Count => count;
        int IStackPooler<T>.Count {
            get => count;
            set => count = value;
        }
        Stack<T> IStackPooler<T>.CreatedPrefabsStack { get; } = new Stack<T>();
    }
    public class ObjectPooler<T> : MonoBehaviour, IStackPooler<T>, IMonoPooler<T> where T : Component, IPoolable<T> {
        public T Prefab;
        T IPooler<T>.Prefab => Prefab;
        public int Max = 32;
        int IPooler<T>.Max => Max;
        private int count = 0;
        public int Count => count;
        int IStackPooler<T>.Count {
            get => count;
            set => count = value;
        }
        Stack<T> IStackPooler<T>.CreatedPrefabsStack { get; } = new Stack<T>();

    }
}
