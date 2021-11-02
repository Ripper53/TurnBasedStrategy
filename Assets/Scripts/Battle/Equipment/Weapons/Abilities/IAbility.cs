using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility {

    public string GetDescription();
    public void Execute(IEnumerable<BattleData> targets);

}
