using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Xml;

public class Map : MonoBehaviour, IEnumerable<Map.Tile> {
    private Tile[,] values;
    public enum Tile {
        None, Wall, Ground
    }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Tile this[int x, int y] {
        get => values[x, y];
        set => values[x, y] = value;
    }
    public Tile this[Vector2Int position] {
        get => values[position.x, position.y];
        set => values[position.x, position.y] = value;
    }

    private readonly WaveFunctionCollapse waveFunctionCollapse = new WaveFunctionCollapse();

    public void SetSize(int width, int height) {
        Width = width;
        Height = height;
        values = new Tile[width, height];
    }

    public void Generate(int width, int height, int n) {
        // Generate enum array for map.
        SetSize(width, height);

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

        Deserialize("Test");

        values = waveFunctionCollapse.Run(this, width, height, n, false, 0);
        Width = width;
        Height = height;

    }

    /// <summary>
    /// We need this for Xml serialization for some reason?????
    /// So just keep it for now!
    /// </summary>
    public void Add(object obj) {
        Debug.LogError("ERROR: USED ADD METHOD!", this);
    }

    private FileStream GetSerializer(string fileName, FileMode fileMode) {
#if UNITY_EDITOR
        Debug.Log(Application.persistentDataPath);
#endif
        string dir = Path.Combine(Application.persistentDataPath, "Maps");
        Directory.CreateDirectory(dir);
        return File.Open(Path.Combine(dir, fileName + ".xml"), fileMode);
    }

    public void Serialize(string fileName) {
        using FileStream file = GetSerializer(fileName, FileMode.OpenOrCreate);
        XmlSerializer ser = new XmlSerializer(typeof(MapData));
        ser.Serialize(file, new MapData(this));
    }

    public void Deserialize(string fileName) {
        using FileStream file = GetSerializer(fileName, FileMode.Open);
        using XmlReader reader = XmlReader.Create(file);
        XmlSerializer ser = new XmlSerializer(typeof(MapData));
        MapData mapData = (MapData)ser.Deserialize(reader);
        SetSize(mapData.Width, mapData.Height);
        for (int y = 0, i = 0; y < mapData.Height; y++) {
            for (int x = 0; x < mapData.Width; x++, i++)
                values[x, y] = mapData.Tiles[i];
        }
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
