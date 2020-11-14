using UnityEngine;
using DG.Tweening;

public class GameOverPanelMoverMono : MonoBehaviour
{

    private void Start()
    {
        AnimateMoveIn();
    }

    private void AnimateMoveIn()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, Screen.currentResolution.height + rectTransform.rect.height);
        rectTransform.DOAnchorPosY(0, Main.Instance.Settings.MenusAnimationSettings.MovementDuration);
    }
}
