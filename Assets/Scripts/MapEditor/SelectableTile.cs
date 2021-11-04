using UnityEngine;
using UnityEngine.EventSystems;

namespace Editor {
    public class SelectableTile : MonoBehaviour, IPointerClickHandler {
        public MapEditor MapEditor;
        public Map.Tile Tile;

        public void OnPointerClick(PointerEventData eventData) {
            MapEditor.SelectedTile = this;
        }

    }
}
