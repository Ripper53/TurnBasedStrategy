using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour {
    public Tilemap Tilemap;

    public MapTiles Tiles;

    public void Build(Map map) {
        // Instantiate the map.
        for (int y = 0; y < map.Height; y++) {
            for (int x = 0; x < map.Width; x++) {
                TileBase tile = map[x, y] switch {
                    Map.Tile.Ground => Tiles.Ground,
                    _ => Tiles.Wall
                };
                Tilemap.SetTile(new Vector3Int(x, y), tile);
            }
        }
        
    }

}
