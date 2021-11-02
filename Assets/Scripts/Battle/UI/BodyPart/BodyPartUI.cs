using Pooler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodyPartUI : ObjectPoolable, IPoolable<BodyPartUI>, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [System.NonSerialized]
    public BattleUnit BattleUnit;
    public BodyPart BodyPart;

    public IPooler<BodyPartUI> Pooler { get; set; }
    protected override void AddedToPool() { }

    public Image Image;
    public Sprite AvailableSprite, HoverSprite, SelectedSprite;

    private bool targeted = false;
    public void OnPointerClick(PointerEventData eventData) {
        if (targeted) {
            targeted = false;
            BattleUnit.Data.CombatData.RemoveTarget(BodyPart);
            Image.sprite = HoverSprite;
        } else {
            targeted = true;
            BattleUnit.Data.CombatData.AddTarget(BodyPart);
            Image.sprite = SelectedSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Image.sprite == SelectedSprite) return;
        Image.sprite = HoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (Image.sprite == SelectedSprite) return;
        Image.sprite = AvailableSprite;
    }

}
