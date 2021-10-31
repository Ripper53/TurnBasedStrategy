using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    public bool IsTurn { get; set; } = true;

    public delegate void FinishedTurnAction();
    public event FinishedTurnAction FinishedTurn;

    public void FinishTurn() => FinishedTurn?.Invoke();

}
