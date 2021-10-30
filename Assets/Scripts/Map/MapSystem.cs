using UnityEngine;

public class MapSystem : MonoBehaviour {
    public Map Map;
    public MapBuilder Builder;
    public int Width = 10, Height = 10;

    protected void Awake() {
        Map.Generate(Width, Height);
        Builder.Build(Map);
    }

}
