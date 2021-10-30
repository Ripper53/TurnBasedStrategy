using UnityEngine;

public class Map : MonoBehaviour {
    private Tile[,] values;
    public enum Tile {
        Wall, Ground
    }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Tile this[int x, int y] => values[x, y];
    public Tile this[Vector2Int position] => values[position.x, position.y];

    public void Generate(int width, int height) {
        // Generate enum array for map.
        Width = width;
        Height = height;

        values = new Tile[width, height];

        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                values[x, y] = Random.Range(0, 2) switch {
                    0 => Tile.Ground,
                    _ => Tile.Wall
                };
            }
        }

    }

    public bool InBounds(Vector2Int position) {
        return position.x >= 0 && position.x < Width &&
               position.y >= 0 && position.y < Height;
    }

}
