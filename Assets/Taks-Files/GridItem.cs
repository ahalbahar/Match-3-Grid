using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GridItem : MonoBehaviour
{
    public Image Icon;
    public int ItemType;

    private void Awake()
    {
        Icon.rectTransform.anchoredPosition = Vector2.up * 110;
    }

    public void Set(int type, Sprite icon, float showDelay)
    {
        Icon.sprite = icon;
        ItemType = type;

        if (Application.isPlaying) Icon.rectTransform.DOAnchorPosY(0, 0.25f).SetDelay(showDelay);
    }
}
