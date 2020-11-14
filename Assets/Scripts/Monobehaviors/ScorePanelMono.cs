using UnityEngine;
using TMPro;

using LanguagesAndTexts;

public class ScorePanelMono : TextsOutputMono_Base
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private ScoreController _controller;

    protected override void Initialize()
    {
        _controller = Main.Instance.ScoreController;
        _controller.OnScoreChanged += ChangeScoreText;
        ChangeScoreText(0);
    }

    private void ChangeScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    protected override void SetTextValues(Texts texts, TMP_FontAsset font)
    {
        _scoreText.font = font;
    }

    protected override void OnGameobjectDestroyed()
    {
        _controller.OnScoreChanged -= ChangeScoreText;
    }
}
