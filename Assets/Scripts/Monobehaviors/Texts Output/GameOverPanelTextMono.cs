using UnityEngine;
using TMPro;
using LanguagesAndTexts;

public class GameOverPanelTextMono : TextsOutputMono_Base
{
    [SerializeField]
    private TextMeshProUGUI _panelLabelText, _scoreText, _scoreValue, _buttonTryAgainText, _buttonMainMenuText;

    private ScoreController _scoreController;

    private Texts _texts;

    protected override void Initialize()
    {
        _scoreController = Main.Instance.ScoreController;
    }

    protected override void SetTextValues(Texts texts, TMP_FontAsset font)
    {
        _buttonTryAgainText.text = texts.TryAgainButtonText;
        _buttonTryAgainText.font = font;

        _buttonMainMenuText.text = texts.BackToMainMenuButtonText;
        _buttonMainMenuText.font = font;

        _panelLabelText.text = texts.GameOverPanelLabelText;
        _panelLabelText.font = font;

        ShowScore(texts, font);
    }

    private void ShowScore(Texts texts, TMP_FontAsset font)
    {
        _scoreText.font = font;
        _scoreValue.font = font;

        int currentScoreRank = _scoreController.CurrentScoreRank;
        if (currentScoreRank < Main.Instance.Settings.HighScoreSettings.HighScoreSlotsCount)
        {
            _scoreText.text = string.Concat(texts.GameOverPanel_NewHighScorePart1, currentScoreRank + 1, texts.GameOverPanel_NewHighScorePart2);
        }
        else
        {
            _scoreText.text = texts.GameOverPanel_FinalScoreIs;
        }

        _scoreValue.text = _scoreController.CurrentScore.ToString();
    }
}
