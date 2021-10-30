using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MapTiles", menuName = "Custom/Map Tiles")]
public class MapTiles : ScriptableObject {
    public TileBase Ground, Wall;
}
