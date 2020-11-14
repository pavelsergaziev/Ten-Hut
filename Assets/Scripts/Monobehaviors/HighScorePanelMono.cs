using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using LanguagesAndTexts;

public class HighScorePanelMono : TextsOutputMono_Base
{
    [SerializeField]
    private TextMeshProUGUI _text;

    private ScoreController _score;
    

    protected override void Initialize()
    {
        _score = Main.Instance.ScoreController;
        RefreshHighScore();
    }

    protected override void SetTextValues(Texts texts, TMP_FontAsset font)
    {
        _text.font = font;
    }

    private void RefreshHighScore()
    {
        int[] highScores = _score.GetHighScoreRankings();

        _text.text = "";

        for (int i = 0; i < highScores.Length; i++)
        {
            _text.text = string.Concat(_text.text, i+1, " - ", highScores[i], "\n");
        }
    }
}
