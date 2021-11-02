using ArtificialIntelligence.Map;
using UnityEngine;

public abstract class MapMindSetup : MonoBehaviour {
    public MapMind MapMind;

    protected void Awake() {
        SetupMapMind(MapMind);
    }

    protected abstract void SetupMapMind(MapMind mapMind);

}
