using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse {
    
    private class Neighbors {
        public readonly Map.Tile Tile;
        public readonly Dictionary<Vector2Int, List<Map.Tile>> requiredNeighbors;
        public Neighbors(Map.Tile tile) {
            Tile = tile;
            requiredNeighbors = new Dictionary<Vector2Int, List<Map.Tile>>();
        }
        public void IsNeighbor(Map.Tile neighboringTile, Vector2Int position) {
            if (requiredNeighbors.TryGetValue(position, out List<Map.Tile> tiles)) {
                if (!tiles.Contains(neighboringTile))
                    tiles.Add(neighboringTile);
            } else {
                requiredNeighbors.Add(position, new List<Map.Tile> {
                    neighboringTile
                });
            }
        }
        public IReadOnlyList<Map.Tile> GetNeighbors(Vector2Int position) {
            return requiredNeighbors[position];
        }
        public bool IsRequired(Vector2Int position, Map.Tile neighbor) {
            if (neighbor == Map.Tile.None || !requiredNeighbors.ContainsKey(position)) return true;
            foreach (Map.Tile tile in requiredNeighbors[position]) {
                if (tile == neighbor)
                    return true;
            }
            return false;
        }
        public void Clear() => requiredNeighbors.Clear();
    }
    private readonly Dictionary<Map.Tile, Neighbors> neighbors = new Dictionary<Map.Tile, Neighbors>(2) {
        { Map.Tile.Wall, new Neighbors(Map.Tile.Wall) },
        { Map.Tile.Ground, new Neighbors(Map.Tile.Ground) }
    };
    /// <summary>
    /// https://www.procjam.com/tutorials/wfc/
    /// </summary>
    /// <param name="map"></param>
    /// <param name="w">Output's width.</param>
    /// <param name="h">Output's height.</param>
    /// <param name="n">Represents the width & height of the patterns that the overlap model breaks the input into.</param>
    /// <param name="periodicOutput"></param>
    /// <param name="symmetry">Represents which additional symmetries of the input pattern are digested.</param>
    /// <returns></returns>
    public Map.Tile[,] Run(Map map, int w, int h, int n, bool periodicOutput, int symmetry) {
        foreach (Neighbors neighbor in neighbors.Values) {
            neighbor.Clear();
        }

        for (int y = 0; y < map.Height; y++) {
            for (int x = 0; x < map.Width; x++) {
                Vector2Int pos = new Vector2Int(x, y);
                foreach (Vector2Int p in GetN()) {
                    Vector2Int neighborPos = pos + p;
                    if (map.InBounds(neighborPos))
                        neighbors[map[x, y]].IsNeighbor(map[neighborPos], p);
                }
            }
        }

        foreach (var pair in neighbors.Values) {
            string a = pair.Tile +": ";
            foreach (var b in pair.requiredNeighbors) {
                a += b.Key + ", ";
                foreach (Map.Tile tile in b.Value)
                    a += tile + "|";
            }
            Debug.Log(a);
        }

        Map.Tile[,] tiles = new Map.Tile[w, h];
        bool TryPlace(Map.Tile tile, int x, int y) {
            foreach (Vector2Int neighborPos in GetN()) {
                Vector2Int p = new Vector2Int(x, y) + neighborPos;
                if (p.x >= 0 && p.x < w && p.y >= 0 && p.y < h) {
                    if (!neighbors[tile].IsRequired(neighborPos, tiles[p.x, p.y])) {
                        Debug.Log($"FAILED: {tile}, {p}");
                        return false;
                    }
                }
            }
            tiles[x, y] = tile;
            return true;
        }
        void TryToPlaceRandomTile(List<Map.Tile> randomTiles, int x, int y, ref int i) {
            do {
                i++;
                int randomIndex = Random.Range(0, randomTiles.Count);
                switch (randomTiles[randomIndex]) {
                    case Map.Tile.Ground:
                        // Ground
                        if (TryPlace(Map.Tile.Ground, x, y))
                            return;
                        break;
                    default:
                        // Wall
                        if (TryPlace(Map.Tile.Wall, x, y))
                            return;
                        break;
                }
                randomTiles[randomIndex] = randomTiles[^1];
                randomTiles.RemoveAt(randomTiles.Count - 1);
            } while (randomTiles.Count != 0);
        }

        int i = 0;
        List<Map.Tile> randomTiles = new List<Map.Tile>(2);
        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w && i < int.MaxValue - 1; x++) {
                randomTiles.Clear();
                randomTiles.AddRange(GetTiles());
                TryToPlaceRandomTile(randomTiles, x, y, ref i);
            }
        }
        Debug.Log("Generate iterations: " + i);
        foreach (Vector2Int b in GetN()) {
            Debug.Log("N: " + b);
        }

        return tiles;
    }
    private IEnumerable<Vector2Int> GetN(int n = 2) {
        bool CheckIfWithinDistance(Vector2Int pos) {
            return (pos.x * pos.x) + (pos.y * pos.y) <= (n * n);
        }
        for (int y = -n; y <= n; y++) {
            for (int x = -n; x <= n; x++) {
                Vector2Int pos = new Vector2Int(x, y);
                if (pos != Vector2Int.zero && CheckIfWithinDistance(pos))
                    yield return pos;
            }
        }
    }
    private IEnumerable<Map.Tile> GetTiles() {
        yield return Map.Tile.Wall;
        yield return Map.Tile.Ground;
    }

}
