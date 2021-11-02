using Pooler;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour {
    public Player PlayerData;
    [System.Serializable]
    public struct Player {
        public CameraFollow CameraFollow;
        public BattleData BattleData;
        public PlayerBattleField BattleField;
        public void Enable(bool value) {
            BattleData.gameObject.SetActive(value);
            CameraFollow.enabled = value;
            Vector3 pos = CameraFollow.Transform.position;
            if (value) {
                pos.x = CameraFollow.Target.position.x;
                pos.y = CameraFollow.Target.position.y;
            } else {
                pos.x = 0f;
                pos.y = 0f;
            }
            CameraFollow.Transform.position = pos;
        }
    }

    public MapSystem MapSystem;
    public BattleUI BattleUI;

    public GameObject BattleObj;
    public BattleUnitPooler UnitPooler;

    public Transform Team1, Team2;

    public bool CommenceBattle(BattleData battleData) {
        if (SpawnUnits(battleData)) {
            Set(true);
            BattleUI.Initialize();
            return true;
        }
        return false;
    }

    public void FinishBattle() {
        foreach (BattleUnit unit in unitsCommencedInBattle)
            unit.AddToPool();
        unitsCommencedInBattle.Clear();
        Set(false);
    }

    private void Set(bool value) {
        PlayerData.Enable(!value);
        MapSystem.Enable(!value);
        BattleObj.SetActive(value);
    }

    private readonly List<BattleUnit> unitsCommencedInBattle = new List<BattleUnit>();
    public IReadOnlyList<BattleUnit> UnitsCommencedInBattle => unitsCommencedInBattle;
    public void RemoveUnitFromBattle(BattleUnit unit) {
        unitsCommencedInBattle.Remove(unit);
        unit.AddToPool();
        if (unitsCommencedInBattle.Count == 1)
            FinishBattle();
    }
    private bool SpawnUnits(BattleData battleData) {
        if (SpawnUnit(PlayerData.BattleData, Team1, out BattleUnit playerUnut)) {
            if (SpawnUnit(battleData, Team2, out BattleUnit unit2)) {
                PlayerData.BattleData.CombatData.AddTarget(battleData);
                battleData.CombatData.AddTarget(PlayerData.BattleData);
                unitsCommencedInBattle.Add(playerUnut);
                unitsCommencedInBattle.Add(unit2);
                return true;
            } else {
                playerUnut.AddToPool();
            }
        }
        return false;
    }

    private bool SpawnUnit(BattleData battleData, Transform parent, out BattleUnit unit) {
        if (((IPooler<BattleUnit>)UnitPooler).Get(out unit)) {
            unit.Data = battleData;
            unit.Data.CombatData.Initialize();
            battleData.BattleSystem = this;
            battleData.BattleUnit = unit;
            unit.SpriteRenderer.sprite = battleData.SpriteRenderer.sprite;

            unit.transform.SetParent(parent);
            unit.transform.localPosition = Vector3.zero;
            unit.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

}
