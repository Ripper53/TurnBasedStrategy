using ArtificialIntelligence.Map;
using UnityEngine;

public class MindCharacterMoveCondition : CharacterMoveCondition {
    public MapMind Mind;

    public override bool CanMove(Character character, Vector2Int position) {
        return base.CanMove(character, position) && !Mind.MindBuilder.IsOccupied(position);
    }

}
