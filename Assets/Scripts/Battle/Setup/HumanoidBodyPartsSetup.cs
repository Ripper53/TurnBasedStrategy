using UnityEngine;

public class HumanoidBodyPartsSetup : BodyPartsSetup {

    protected override void SetupBodyParts(BattleData battleData) {
        battleData.AddBodyPart(new BodyPart("Head", new Vector2(0f, 1f)));
        battleData.AddBodyPart(new BodyPart("Torso", new Vector2(0f, 0f)));
        battleData.AddBodyPart(new BodyPart("Arms", new Vector2(1f, 0f)));
        battleData.AddBodyPart(new BodyPart("Feet", new Vector2(0f, -1f)));
    }

}
