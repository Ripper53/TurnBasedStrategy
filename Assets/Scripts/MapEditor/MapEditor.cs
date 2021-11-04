using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Editor {
    public class MapEditor : MonoBehaviour, IPointerClickHandler {
        public Map Map;
        public Camera Camera;
        public RectTransform RectTransform;

        public SelectableTile SelectedTile;

        public void SetDimensions(Vector2Int size) {
            RectTransform.position = new Vector2(size.x, size.y) * 0.5f;
            RectTransform.sizeDelta = size;
            Map.SetSize(size.x, size.y);
        }

        protected void Awake() {
            SetDimensions(new Vector2Int(10, 10));
        }

        public void OnPointerClick(PointerEventData eventData) {
            Vector2Int tilePos = Vector2Int.RoundToInt(Camera.ScreenToWorldPoint(eventData.position));
            if (Map.InBounds(tilePos))
                Map[tilePos] = SelectedTile.Tile;
        }

        public void Save() {
            Map.Serialize("Test");
        }

    }
}
