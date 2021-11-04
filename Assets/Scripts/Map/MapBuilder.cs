using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour {
    public Character PlayerCharacter;
    public Tilemap Tilemap;

    public SpriteRenderer GroundPrefab, WallPrefab;

    public MapTiles Tiles;

    private readonly List<Vector2Int> groundPosition = new List<Vector2Int>();
    public bool GetRandomGroundPosition(out Vector2Int position) {
        if (groundPosition.Count == 0) {
            position = default;
            return false;
        }
        int index = Random.Range(0, groundPosition.Count);
        position = groundPosition[index];
        groundPosition[index] = groundPosition[^1];
        groundPosition.RemoveAt(groundPosition.Count - 1);
        return true;
    }

    public void Build(Map map) {
        Tilemap.ClearAllTiles();
        // Instantiate the map.
        for (int y = 0; y < map.Height; y++) {
            for (int x = 0; x < map.Width; x++) {
                TileBase tile = map[x, y] switch {
                    Map.Tile.Ground => Tiles.Ground,
                    _ => Tiles.Wall
                };
                Tilemap.SetTile(new Vector3Int(x, y), tile);

                SpriteRenderer prefab;
                switch (map[x, y]) {
                    case Map.Tile.Ground:
                        prefab = GroundPrefab;
                        groundPosition.Add(new Vector2Int(x, y));
                        break;
                    case Map.Tile.Wall:
                        prefab = WallPrefab;
                        break;
                    default:
                        Debug.Log("NONE TILE!");
                        continue;
                }

                SpriteRenderer sr = Instantiate(prefab, new Vector3(x, y), Quaternion.identity, Tilemap.transform.parent);
                sr.gameObject.SetActive(true);
            }
        }

        if (GetRandomGroundPosition(out Vector2Int position))
            PlayerCharacter.SetInitialPosition(position);

    }

}
