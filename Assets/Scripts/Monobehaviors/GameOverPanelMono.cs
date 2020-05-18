using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverPanelMono : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreRankingsText, _scoreValue;

    private ScoreController _scoreController;

    private void Start()
    {
        _scoreController = Main.Instance.ScoreController;

        ShowScore();
        AnimateMoveIn();
    }

    private void ShowScore()
    {
        int currentScoreRank = _scoreController.CurrentScoreRank;
        if (currentScoreRank < Main.Instance.Settings.HighScoreSettings.HighScoreSlotsCount)
        {
            _scoreRankingsText.text = string.Concat("New high score! You've reached rank ", currentScoreRank + 1, " with a score of");            
        }
        else
        {
            _scoreRankingsText.text = "Your final score is ";
        }

        _scoreValue.text = _scoreController.CurrentScore.ToString();
    }


    private void AnimateMoveIn()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, Screen.currentResolution.height + rectTransform.rect.height);
        rectTransform.DOAnchorPosY(0, Main.Instance.Settings.MenusAnimationSettings.MovementDuration);
    }
}
