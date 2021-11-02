using UnityEngine;

public class Character : MonoBehaviour {
    public Map Map;
    public BattleData BattleData;
    public CharacterMoveCondition MoveCondition;

    public Vector2Int Position { get; private set; }

    public void SetInitialPosition(Vector2Int position) => Position = position;

    private bool MoveBy(Vector2Int amount) {
        Vector2Int newPos = Position + amount;
        if (MoveCondition.CanMove(this, newPos)) {
            Position = newPos;
            return true;
        }
        return false;
    }

    public bool MoveUp() {
        return MoveBy(new Vector2Int(0, 1));
    }

    public bool MoveRight() {
        return MoveBy(new Vector2Int(1, 0));
    }

    public bool MoveDown() {
        return MoveBy(new Vector2Int(0, -1));
    }

    public bool MoveLeft() {
        return MoveBy(new Vector2Int(-1, 0));
    }

}
