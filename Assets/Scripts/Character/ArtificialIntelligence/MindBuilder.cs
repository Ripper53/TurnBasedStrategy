using ArtificialIntelligence;
using ArtificialIntelligence.Works;
using Pooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindBuilder : MonoBehaviour {
    public PlayerCharacter PlayerCharacter;
    public MindPooler MindPooler;
    public MapBuilder MapBuilder;

    private readonly Dictionary<Vector2Int, Mind> minds = new Dictionary<Vector2Int, Mind>();
    public bool IsOccupied(Vector2Int position) => minds.ContainsKey(position);
    public bool GetOccupation(Vector2Int position, out Mind mind) {
        if (minds.TryGetValue(position, out mind))
            return true;
        return false;
    }

    private readonly List<Mind> executionOrderOfMinds = new List<Mind>();

    protected void Start() {
        PlayerCharacter.FinishedTurn += PlayerCharacter_FinishedTurn;
        for (int i = 0; i < 4; i++) {
            if (MapBuilder.GetRandomGroundPosition(out Vector2Int position))
                Create(position);
        }
    }

    private readonly List<Mind> mindsWhichDidNotMakeAMove = new List<Mind>();
    private readonly List<Mind> mindsWhichStillDidNotMakeAMoveForSomeReason = new List<Mind>();
    private struct Comparer : IComparer<Mind> {
        public int Compare(Mind x, Mind y) {
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
    private bool ExecuteMind(Mind mind) {
        Vector2Int oldPosition = mind.Character.Position;
        mind.Execute();
        if (oldPosition != mind.Character.Position) {
            minds.Remove(oldPosition);
            minds.Add(mind.Character.Position, mind);
            if (mind.Character.Position == MapBuilder.PlayerCharacter.Position)
                Debug.Log("FORCE ENGAGE IN BATTLE WITH " + mind.name, mind);
            return true;
        }
        return false;
    }

    private IEnumerator Move() {
        executionOrderOfMinds.Sort(new Comparer());

        foreach (Mind mind in executionOrderOfMinds) {
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

                foreach (Mind mind in mindsWhichDidNotMakeAMove) {
                    if (!ExecuteMind(mind))
                        mindsWhichStillDidNotMakeAMoveForSomeReason.Add(mind);
                    yield return new WaitForSeconds(1f);
                }
            } while (mindsWhichStillDidNotMakeAMoveForSomeReason.Count != oldCount);

            mindsWhichDidNotMakeAMove.Clear();
        }



        PlayerCharacter.IsTurn = true;
    }

    public void Create(Vector2Int position) {
        Create(out Mind mind, position);
        mind.Add(new RandomMovementMindWork());
        mind.gameObject.SetActive(true);
    }

    public bool Create(out Mind mind, Vector2Int position) {
        if (((IPooler<Mind>)MindPooler).Get(out mind)) {
            mind.MindBuilder = this;
            mind.Character.SetInitialPosition(position);
            minds.Add(position, mind);
            executionOrderOfMinds.Add(mind);
            return true;
        }
        return false;
    }

}
