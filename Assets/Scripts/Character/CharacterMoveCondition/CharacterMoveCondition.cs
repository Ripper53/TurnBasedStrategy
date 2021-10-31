using UnityEngine;

public class CharacterMoveCondition : MonoBehaviour {

    public virtual bool CanMove(Character character, Vector2Int position) {
        return character.Map.InBounds(position) && character.Map[position] == Map.Tile.Ground;
    }

}
