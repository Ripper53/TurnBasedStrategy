using Pooler;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Editor {
    public class MapEditor : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public Map Map;
        public Camera Camera;
        public RectTransform RectTransform;

        public SelectableTile SelectedTile;
        public PlacedTilePooler PlacedTilePooler;

        public void SetDimensions(Vector2Int size) {
            RectTransform.position = (new Vector2(size.x, size.y) * 0.5f) - new Vector2(0.5f, 0.5f);
            RectTransform.sizeDelta = size;
            Map.SetSize(size.x, size.y);
            PlacedTilePooler.Max = size.x * size.y;
        }

        protected void Awake() {
            SetDimensions(new Vector2Int(10, 10));
        }

        private readonly Dictionary<Vector2Int, PlacedTile> placedTiles = new Dictionary<Vector2Int, PlacedTile>();
        private bool GetPlacedTilePrefab(out PlacedTile placedTile) => ((IPooler<PlacedTile>)PlacedTilePooler).Get(out placedTile);

        public void Save() {
            Map.Serialize("Test");
        }
        public void Clear() {
            foreach (PlacedTile tile in placedTiles.Values)
                tile.AddToPool();
            placedTiles.Clear();
        }

        private void PlaceTile(PointerEventData eventData) {
            Vector2Int tilePos = Vector2Int.RoundToInt(Camera.ScreenToWorldPoint(eventData.position));
            if (Map.InBounds(tilePos)) {
                PlacedTile placedTile;
                switch (eventData.button) {
                    case PointerEventData.InputButton.Left:
                        if (placedTiles.ContainsKey(tilePos)) {
                            Map[tilePos] = SelectedTile.Tile;
                            placedTiles[tilePos].transform.position = (Vector2)tilePos;
                            placedTiles[tilePos].Set(SelectedTile.Tile);
                        } else if (GetPlacedTilePrefab(out placedTile)) {
                            placedTiles.Add(tilePos, placedTile);
                            placedTile.Set(SelectedTile.Tile);
                            placedTile.transform.SetParent(transform.parent);
                            placedTile.transform.position = (Vector2)tilePos;
                            placedTile.gameObject.SetActive(true);
                        }
                        break;
                    case PointerEventData.InputButton.Right:
                        if (placedTiles.TryGetValue(tilePos, out placedTile)) {
                            placedTiles.Remove(tilePos);
                            placedTile.AddToPool();
                        }
                        Map[tilePos] = Map.Tile.None;
                        break;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            PlaceTile(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData) {
            
        }

        public void OnDrag(PointerEventData eventData) {
            PlaceTile(eventData);
        }

        public void OnEndDrag(PointerEventData eventData) {
            
        }

    }
}
