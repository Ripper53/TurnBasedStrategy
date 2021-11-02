using ArtificialIntelligence.Battle;
using ArtificialIntelligence.Battle.Works;
using ArtificialIntelligence.Map;
using ArtificialIntelligence.Map.Works;
using Pooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMindBuilder : MonoBehaviour {
    public PlayerCharacter PlayerCharacter;
    public MapBuilder MapBuilder;
    public BattleSystem BattleSystem;

    public SpookyData Spooky;

    private readonly Dictionary<Vector2Int, MapMind> minds = new Dictionary<Vector2Int, MapMind>();
    public bool IsOccupied(Vector2Int position) => minds.ContainsKey(position);
    public bool GetOccupation(Vector2Int position, out MapMind mind) {
        if (minds.TryGetValue(position, out mind))
            return true;
        return false;
    }
    public void DestroyMapMindAt(Vector2Int position) {
        Debug.Log(position);
        int index = executionOrderOfMinds.IndexOf(minds[position]);
        executionOrderOfMinds[index] = executionOrderOfMinds[^1];
        executionOrderOfMinds.RemoveAt(executionOrderOfMinds.Count - 1);
        minds[position].AddToPool();
        minds.Remove(position);
    }

    private readonly List<MapMind> executionOrderOfMinds = new List<MapMind>();

    protected void Start() {
        PlayerCharacter.FinishedTurn += PlayerCharacter_FinishedTurn;
        for (int i = 0; i < 4; i++) {
            if (MapBuilder.GetRandomGroundPosition(out Vector2Int position))
                Create(Character.Spooky, position);
        }
    }

    private readonly List<MapMind> mindsWhichDidNotMakeAMove = new List<MapMind>();
    private readonly List<MapMind> mindsWhichStillDidNotMakeAMoveForSomeReason = new List<MapMind>();
    private struct Comparer : IComparer<MapMind> {
        public int Compare(MapMind x, MapMind y) {
            if (x.Character.Position.y < y.Character.Position.y)
                return -1;
            else if (x.Character.Position.y > y.Character.Position.y)
                return 1;
            else if (x.Character.Position.x < y.Character.Position.x)
                return -1;
            else
                return 1;
        }
    }
    private void PlayerCharacter_FinishedTurn() {
        StartCoroutine(Move());
    }
    private bool ExecuteMind(MapMind mind) {
        Vector2Int oldPosition = mind.Character.Position;
        mind.Execute();
        if (oldPosition != mind.Character.Position) {
            minds.Remove(oldPosition);
            minds.Add(mind.Character.Position, mind);
            if (mind.Character.Position == MapBuilder.PlayerCharacter.Position) {
                BattleSystem.CommenceBattle(mind.Character.BattleData);
            }
            return true;
        }
        return false;
    }

    private IEnumerator Move() {
        executionOrderOfMinds.Sort(new Comparer());

        foreach (MapMind mind in executionOrderOfMinds) {
            if (!ExecuteMind(mind))
                mindsWhichDidNotMakeAMove.Add(mind);
                
            yield return new WaitForSeconds(0.1f);
        }

        if (mindsWhichDidNotMakeAMove.Count != 0) {
            int oldCount;
            mindsWhichStillDidNotMakeAMoveForSomeReason.AddRange(mindsWhichDidNotMakeAMove);
            do {
                oldCount = mindsWhichStillDidNotMakeAMoveForSomeReason.Count;
                mindsWhichStillDidNotMakeAMoveForSomeReason.Clear();

                foreach (MapMind mind in mindsWhichDidNotMakeAMove) {
                    if (!ExecuteMind(mind))
                        mindsWhichStillDidNotMakeAMoveForSomeReason.Add(mind);
                    yield return new WaitForSeconds(1f);
                }
            } while (mindsWhichStillDidNotMakeAMoveForSomeReason.Count != oldCount);

            mindsWhichDidNotMakeAMove.Clear();
        }



        PlayerCharacter.IsTurn = true;
    }

    [System.Serializable]
    public struct SpookyData {
        public MapMindPooler Pooler;
    }

    public enum Character {
        Spooky
    }

    public void Create(Character character, Vector2Int position) {
        MapMind mind;
        switch (character) {
            default:
                Create(Spooky.Pooler, out mind, position);
                break;
        }
        mind.gameObject.SetActive(true);
    }

    public bool Create(IPooler<MapMind> mindPooler, out MapMind mind, Vector2Int position) {
        if (mindPooler.Get(out mind)) {
            mind.transform.SetParent(MapBuilder.Tilemap.transform.parent);
            mind.MindBuilder = this;
            mind.Character.SetInitialPosition(position);
            minds.Add(position, mind);
            executionOrderOfMinds.Add(mind);
            return true;
        }
        return false;
    }

}
