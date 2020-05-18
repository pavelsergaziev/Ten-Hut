using UnityEngine;
using TMPro;

public class ScorePanelMono : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private ScoreController _controller;
    

    private void Start()
    {
        _controller = Main.Instance.ScoreController;
        _controller.OnScoreChanged += ChangeScoreText;
        ChangeScoreText(0);
    }

    private void ChangeScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void OnDestroy()
    {
        _controller.OnScoreChanged -= ChangeScoreText;
    }
}
