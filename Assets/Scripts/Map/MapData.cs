using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Map")]
public class MapData {
    [XmlAttribute]
    public int Width, Height;

    [XmlArray("Tiles")]
    public List<Map.Tile> Tiles;
    public MapData() {
        Tiles = new List<Map.Tile>();
    }
    public MapData(Map map) {
        Width = map.Width;
        Height = map.Height;
        Tiles = new List<Map.Tile>(Width * Height);
        foreach (Map.Tile tile in map)
            Tiles.Add(tile);
    }
}
