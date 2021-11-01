using UnityEngine;

public class MapSystem : MonoBehaviour {
    public PlayerCharacterInput PlayerCharacterInput;
    public Map Map;
    public MapBuilder Builder;
    public MapMindBuilder MapMindBuilder;
    public int Width = 10, Height = 10;

    public GameObject MapObj;

    protected void Awake() {
        Map.Generate(Width, Height);
        Builder.Build(Map);
    }

    public void Enable(bool value) {
        PlayerCharacterInput.enabled = value;
        MapObj.SetActive(value);
    }

    public void RemoveCharacterFromMap(Character character) {
        MapMindBuilder.DestroyMapMindAt(character.Position);
    }

}
