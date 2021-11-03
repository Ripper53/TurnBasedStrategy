using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse {

    private class Neighbors {
        public readonly Map.Tile Tile;
        private readonly Dictionary<Vector2Int, List<Map.Tile>> requiredNeighbors;
        public Neighbors(Map.Tile tile) {
            Tile = tile;
            requiredNeighbors = new Dictionary<Vector2Int, List<Map.Tile>>();
        }
        public void IsNeighbor(Map.Tile tile, Vector2Int position) {
            if (requiredNeighbors.TryGetValue(position, out List<Map.Tile> tiles)) {
                if (!tiles.Contains(tile))
                    tiles.Add(tile);
            } else {
                requiredNeighbors.Add(position, new List<Map.Tile> {
                    tile
                });
            }
        }
        public IReadOnlyList<Map.Tile> GetNeighbors(Vector2Int position) {
            return requiredNeighbors[position];
        }
        public struct Neighbor {
            public readonly Map.Tile Tile;
            public readonly Vector2Int Position;
            public Neighbor(Map.Tile tile, Vector2Int position) {
                Tile = tile;
                Position = position;
            }
        }
        public bool IsRequired(Map map, Vector2Int position, IEnumerable<Neighbor> neighbors) {
            bool A(Vector2Int pos, List<Map.Tile> tiles) {
                foreach (Map.Tile tile in tiles) {
                    if (map[pos] == tile || map[pos] == Map.Tile.None)
                        return true;
                }
                return false;
            }
            foreach (KeyValuePair<Vector2Int, List<Map.Tile>> pair in requiredNeighbors) {
                Vector2Int pos = position + pair.Key;
                if (map.InBounds(pos) && !A(pos, pair.Value))
                    return false;
            }
            return true;
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

        IEnumerable<Neighbors.Neighbor> GetNeighbors(Vector2Int position) {
            foreach (Vector2Int pos in GetN())
                yield return new Neighbors.Neighbor(map[position + pos], pos);
        }
        Map.Tile[,] tiles = new Map.Tile[w, h];
        bool TryPlace(Map.Tile tile, int x, int y) {
            foreach (Vector2Int neighborPos in GetN()) {
                Vector2Int p = new Vector2Int(x, y) + neighborPos;
                if (map.InBounds(p)) {
                    if (!neighbors[tile].IsRequired(map, p, GetNeighbors(p)))
                        return false;
                }
            }
            tiles[x, y] = tile;
            return true;
        }
        void A(List<Map.Tile> randomTiles, int x, int y, ref int i) {
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
                A(randomTiles, x, y, ref i);
            }
        }
        Debug.Log("Generate iterations: " + i);

        return tiles;
    }
    private IEnumerable<Vector2Int> GetN() {
        yield return new Vector2Int(0, 1);
        yield return new Vector2Int(1, 0);
        yield return new Vector2Int(0, -1);
        yield return new Vector2Int(-1, 0);
    }
    private IEnumerable<Map.Tile> GetTiles() {
        yield return Map.Tile.Wall;
        yield return Map.Tile.Ground;
    }

}
