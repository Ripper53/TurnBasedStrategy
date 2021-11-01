using System.Collections.Generic;
using UnityEngine;

namespace ArtificialIntelligence.Map.Works {
    public class RandomMovementMapWork : MapWork {

        private enum Direction {
            Up, Right, Down, Left
        }
        private readonly List<Direction> directions = new List<Direction>(4);
        public override IEnumerator<IMindInstruction> Run() {
            directions.Clear();
            directions.Add(Direction.Up);
            directions.Add(Direction.Right);
            directions.Add(Direction.Down);
            directions.Add(Direction.Left);

            do {
                int index = Random.Range(0, directions.Count);
                Direction dir = directions[index];
                switch (dir) {
                    case Direction.Up:
                        if (Mind.Character.MoveUp())
                            yield break;
                        break;
                    case Direction.Right:
                        if (Mind.Character.MoveRight())
                            yield break;
                        break;
                    case Direction.Down:
                        if (Mind.Character.MoveDown())
                            yield break;
                        break;
                    default:
                        if (Mind.Character.MoveLeft())
                            yield break;
                        break;
                }
                directions.RemoveAt(index);
            } while (directions.Count != 0);

        }

    }
}
