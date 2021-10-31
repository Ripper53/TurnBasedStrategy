using ArtificialIntelligence;
using UnityEngine;

public class MindCharacterMoveCondition : CharacterMoveCondition {
    public Mind Mind;

    public override bool CanMove(Character character, Vector2Int position) {
        return base.CanMove(character, position) && !Mind.MindBuilder.IsOccupied(position);
    }

}
