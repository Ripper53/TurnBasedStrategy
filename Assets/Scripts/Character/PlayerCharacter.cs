using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    public delegate void FinishedTurnAction();
    public event FinishedTurnAction FinishedTurn;

    public void FinishTurn() => FinishedTurn?.Invoke();

}
