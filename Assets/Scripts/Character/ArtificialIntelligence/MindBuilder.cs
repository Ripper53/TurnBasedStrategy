using ArtificialIntelligence;
using ArtificialIntelligence.Works;
using System.Collections.Generic;
using UnityEngine;

public class MindBuilder : MonoBehaviour {
    public PlayerCharacter PlayerCharacter;
    public Character CharacterPrefab;

    private readonly List<Mind> minds = new List<Mind>();

    protected void Awake() {
        PlayerCharacter.FinishedTurn += PlayerCharacter_FinishedTurn;
        Create();
    }

    private void PlayerCharacter_FinishedTurn() {
        foreach (Mind mind in minds)
            mind.Execute();
    }

    public void Create() {
        Character character = Instantiate(CharacterPrefab);
        Mind mind = character.GetComponent<Mind>();
        mind.Add(new RandomMovementMindWork());
        minds.Add(mind);
    }

}
