using UnityEngine;

public class HumanoidBodyPartsSetup : BodyPartsSetup {
    public PositionData Positions;
    [System.Serializable]
    public struct PositionData {
        public Vector2
            Head,
            Torso,
            Arms,
            Feet;
    }
    public SizeData Sizes;
    [System.Serializable]
    public struct SizeData {
        public Vector2
            Head,
            Torso,
            Arms,
            Feet;
    }

    protected override void SetupBodyParts(BattleData battleData) {
        battleData.AddBodyPart(new BodyPart("Head", Positions.Head, Sizes.Head));
        battleData.AddBodyPart(new BodyPart("Torso", Positions.Torso, Sizes.Torso));
        battleData.AddBodyPart(new BodyPart("Arms", Positions.Arms, Sizes.Arms));
        battleData.AddBodyPart(new BodyPart("Feet", Positions.Feet, Sizes.Feet));
    }

}
