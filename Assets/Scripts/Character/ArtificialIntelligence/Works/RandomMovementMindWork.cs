using System.Collections.Generic;
using UnityEngine;

namespace ArtificialIntelligence.Works {
    public class RandomMovementMindWork : MindWork {

        public override IEnumerator<IMindInstruction> Run() {
            if (Random.Range(0, 2) == 0) {
                // Horizontal
                Mind.Character.MoveBy(new Vector2Int(Random.Range(0, 2) == 0 ? 1 : -1, 0));
            } else {
                // Vertical
                Mind.Character.MoveBy(new Vector2Int(0, Random.Range(0, 2) == 0 ? 1 : -1));
            }
            yield break;
        }

    }
}
