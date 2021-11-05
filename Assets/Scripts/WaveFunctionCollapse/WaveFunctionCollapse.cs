using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse {
    
    private class Neighbors {
        public readonly Map.Tile Tile;
        public readonly Dictionary<Vector2Int, List<Map.Tile>> possibleNeighbors;
        private readonly Dictionary<Map.Tile, int> requiredNeighbors;
        public Neighbors(Map.Tile tile) {
            Tile = tile;
            possibleNeighbors = new Dictionary<Vector2Int, List<Map.Tile>>();
            requiredNeighbors = new Dictionary<Map.Tile, int>(2) {
                { Map.Tile.Wall, 0 },
                { Map.Tile.Ground, 0 }
            };
        }
        public void ReinitializeForNewTile() {
            alreadyChecksIfRequired.Clear();
        }
        private readonly HashSet<Map.Tile> alreadyChecksIfRequired = new HashSet<Map.Tile>();
        public void IsNeighbor(Map.Tile neighboringTile, Vector2Int position) {
            if (alreadyChecksIfRequired.Add(neighboringTile))
                requiredNeighbors[neighboringTile]++;
            if (possibleNeighbors.TryGetValue(position, out List<Map.Tile> tiles)) {
                if (!tiles.Contains(neighboringTile))
                    tiles.Add(neighboringTile);
            } else {
                possibleNeighbors.Add(position, new List<Map.Tile> {
                    neighboringTile
                });
            }
        }
        public IReadOnlyList<Map.Tile> GetNeighbors(Vector2Int position) {
            return possibleNeighbors[position];
        }
        public bool IsRequired(Vector2Int position, Map.Tile neighbor, int max) {
            if (neighbor == Map.Tile.None) return true;
            Debug.Log($"COUNT, {Tile}, {neighbor}: {requiredNeighbors[neighbor]} >= {max}");
            if (requiredNeighbors[neighbor] < max)
                return false;
            if (!possibleNeighbors.ContainsKey(position)) return false;
            foreach (Map.Tile tile in possibleNeighbors[position]) {
                if (tile == neighbor)
                    return true;
            }
            return false;
        }
        public void Clear() {
            possibleNeighbors.Clear();
            requiredNeighbors.Clear();
            requiredNeighbors.Add(Map.Tile.Wall, 0);
            requiredNeighbors.Add(Map.Tile.Ground, 0);
        }
    }
    private readonly Dictionary<Map.Tile, Neighbors> neighbors = new Dictionary<Map.Tile, Neighbors>(2) {
        { Map.Tile.Wall, new Neighbors(Map.Tile.Wall) },
        { Map.Tile.Ground, new Neighbors(Map.Tile.Ground) }
    };
    private class TileRatio {
        public readonly Map.Tile Tile;
        public readonly int Capacity;
        /// <summary>
        /// True if tile ratio will be counted, otherwise false if tile cannot be placed.
        /// </summary>
        public bool IsOpen;
        public int Count;
        public float Percentage => (float)Count / Capacity;
        public TileRatio(Map.Tile tile, int capacity) {
            Tile = tile;
            Capacity = capacity;
            Count = 0;
            IsOpen = true;
        }
    }
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

        TileRatio
            groundRatio = new TileRatio(Map.Tile.Wall, map.Width * map.Height),
            wallRatio = new TileRatio(Map.Tile.Ground, map.Width * map.Height);
        TileRatio[] tileRatios = new TileRatio[] {
            groundRatio, wallRatio
        };
        Dictionary<Map.Tile, TileRatio> max = new Dictionary<Map.Tile, TileRatio> {
            { Map.Tile.Ground, groundRatio },
            { Map.Tile.Wall, wallRatio }
        };
        for (int y = 0; y < map.Height; y++) {
            for (int x = 0; x < map.Width; x++) {
                Vector2Int pos = new Vector2Int(x, y);
                neighbors[map[x, y]].ReinitializeForNewTile();
                foreach (Vector2Int p in GetN(n)) {
                    Vector2Int neighborPos = pos + p;
                    if (map.InBounds(neighborPos))
                        neighbors[map[x, y]].IsNeighbor(map[neighborPos], p);
                }
                switch (map[x, y]) {
                    case Map.Tile.Ground:
                        groundRatio.Count++;
                        break;
                    default:
                        wallRatio.Count++;
                        break;
                }
            }
        }

        foreach (var pair in neighbors.Values) {
            string a = pair.Tile +": ";
            foreach (var b in pair.possibleNeighbors) {
                a += b.Key + ", ";
                foreach (Map.Tile tile in b.Value)
                    a += tile + "|";
            }
            Debug.Log(a);
        }

        Map.Tile[,] tiles = new Map.Tile[w, h];
        bool TryPlace(Map.Tile tile, int x, int y) {
            foreach (Vector2Int neighborPos in GetN(n)) {
                Vector2Int p = new Vector2Int(x, y) + neighborPos;
                if (p.x >= 0 && p.x < w && p.y >= 0 && p.y < h) {
                    if (!neighbors[tile].IsRequired(neighborPos, tiles[p.x, p.y], max[tile].Count)) {
                        Debug.Log($"FAILED: {tile}, {p}");
                        return false;
                    }
                }
            }
            tiles[x, y] = tile;
            return true;
        }
        TileRatio GetTileFromRatio(float v) {
            float per = 0f;
            foreach (TileRatio ratio in tileRatios) {
                per += ratio.Percentage;
                if (ratio.IsOpen && per <= v)
                    return ratio;
            }
            return tileRatios[^1];
        }
        void TryToPlaceRandomTile(TileRatio[] tileRatio, int x, int y, ref int i) {
            for (int k = 0; k < tileRatio.Length; k++) {
                i++;
                float r = Random.Range(0f, 1f);
                TileRatio ratio = GetTileFromRatio(r);
                if (TryPlace(ratio.Tile, x, y))
                    return;
                ratio.IsOpen = false;
            }
        }
        float temp = 0f;
        foreach (TileRatio tileRatio in tileRatios) {
            temp += tileRatio.Percentage;
        }
        Debug.Log("PER: " + temp);
        int i = 0;
        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w && i < int.MaxValue - 1; x++) {
                TryToPlaceRandomTile(tileRatios, x, y, ref i);
                foreach (TileRatio ratio in tileRatios) {
                    ratio.IsOpen = true;
                }
            }
        }
        Debug.Log("Generate iterations: " + i);
        foreach (Vector2Int b in GetN(n)) {
            Debug.Log("N: " + b);
        }

        return tiles;
    }
    private IEnumerable<Vector2Int> GetN(int n) {
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
