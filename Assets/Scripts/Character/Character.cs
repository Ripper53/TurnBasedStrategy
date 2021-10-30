using UnityEngine;

public class Character : MonoBehaviour {
    public Map Map;

    public Vector2Int Position { get; private set; }

    public bool MoveBy(Vector2Int amount) {
        Vector2Int newPos = Position + amount;
        if (Map.InBounds(newPos) && Map[newPos] == Map.Tile.Ground) {
            Position = newPos;
            return true;
        }
        return false;
    }

}
