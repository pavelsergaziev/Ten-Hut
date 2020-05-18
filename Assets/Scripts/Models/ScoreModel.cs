using UnityEngine;

public class ScoreModel
{
    public int[] GetHighScores()
    {
        int highScoreSlotsCount = Main.Instance.Settings.HighScoreSettings.HighScoreSlotsCount;
        int[] highScores = new int[highScoreSlotsCount];

        for (int i = highScoreSlotsCount - 1; i >= 0; i--)
        {
            highScores[i] = PlayerPrefs.GetInt(string.Concat("HighScore", i));
        }

        return highScores;
    }

    public void SaveHighScores(int[] newHighScore)
    {
        int highScoreSlotsCount = Main.Instance.Settings.HighScoreSettings.HighScoreSlotsCount;
        for (int i = 0; i < highScoreSlotsCount; i++)
        {
            PlayerPrefs.SetInt(string.Concat("HighScore", i), newHighScore[i]);
        }

        PlayerPrefs.Save();
    }

    public void ResetHighScores()
    {
        SaveHighScores(new int[Main.Instance.Settings.HighScoreSettings.HighScoreSlotsCount]);
    }
}
