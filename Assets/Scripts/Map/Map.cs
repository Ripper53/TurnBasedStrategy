using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class Map : MonoBehaviour, IEnumerable<Map.Tile> {
    private Tile[,] values;
    public enum Tile {
        None, Wall, Ground
    }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Tile this[int x, int y] => values[x, y];
    public Tile this[Vector2Int position] => values[position.x, position.y];

    private readonly WaveFunctionCollapse waveFunctionCollapse = new WaveFunctionCollapse();

    public void Generate(int width, int height) {
        // Generate enum array for map.
        Width = width;
        Height = height;

        values = new Tile[width, height];

        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                values[x, y] = Random.Range(0, 2) switch {
                    0 => Tile.Ground,
                    _ => Tile.Ground
                };
            }
        }
        values[1, 1] = Tile.Wall;
        values[2, 1] = Tile.Wall;

        XmlSerializer ser = new XmlSerializer(typeof(MapData));
        Debug.Log(Application.persistentDataPath);
        using FileStream file = File.OpenWrite(Path.Combine(Application.persistentDataPath, "Test.xml"));
        ser.Serialize(file, new MapData(this));

        values = waveFunctionCollapse.Run(this, Width, Height, 0, false, 0);

    }
    public void Add(System.Object obj) {
        Debug.LogError("ERROR: USED ADD METHOD!");
    }

    public bool InBounds(Vector2Int position) {
        return position.x >= 0 && position.x < Width &&
               position.y >= 0 && position.y < Height;
    }

    public IEnumerator<Tile> GetEnumerator() {
        foreach (Tile tile in values)
            yield return tile;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

}
