using Pooler;
using UnityEngine;

public class BattleUnit : MonoBehaviour, IPoolable<BattleUnit> {
    public SpriteRenderer SpriteRenderer;
    public Transform UIParent;
    [System.NonSerialized]
    public BattleData Data;

    public IPooler<BattleUnit> Pooler { get; set; }
    public void AddToPool() => Pooler.Add(this);
}
