using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScorePanelMono : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    private ScoreController _score;
    

    private void Start()
    {
        _text.text = "HIGH SCORES\n\n";

        _score = Main.Instance.ScoreController;
        RefreshHighScore();
    }

    private void RefreshHighScore()
    {
        int[] highScores = _score.GetHighScoreRankings();

        for (int i = 0; i < highScores.Length; i++)
        {
            _text.text = string.Concat(_text.text, i+1, " - ", highScores[i], "\n");
        }
    }
}
