using Pooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Editor {
    public class PlacedTile : ObjectPoolable, IPoolable<PlacedTile> {
        public MapEditor MapEditor;
        public Image Image;

        public IPooler<PlacedTile> Pooler { get; set; }
        protected override void AddedToPool() {

        }

        public Sprite
            WallSprite, GroundSprite;
        public void Set(Map.Tile tile) {
            switch (tile) {
                case Map.Tile.Wall:
                    Image.sprite = WallSprite;
                    break;
                case Map.Tile.Ground:
                    Image.sprite = GroundSprite;
                    break;
            }
        }

    }
}
