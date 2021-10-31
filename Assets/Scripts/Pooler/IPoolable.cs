namespace Pooler {
    public interface IPoolable {
        public void AddToPool();
    }
    public interface IPoolable<T> : IPoolable where T : IPoolable<T> {
        public IPooler<T> Pooler { get; set; }
    }
}
