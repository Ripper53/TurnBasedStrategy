using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction : MonoBehaviour {
    public BattleOptions BattleOptions;

    public void Execute() {
        Run();
        BattleOptions.PlayerMadeMove();
    }
    protected abstract void Run();

}
