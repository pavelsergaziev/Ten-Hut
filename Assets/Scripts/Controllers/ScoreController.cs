using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController
{
    private int _currentScore;
    public int CurrentScore { get { return _currentScore; } }

    private int _currentScoreRank;
    public int CurrentScoreRank { get { return _currentScoreRank; } }

    private int _currentScoreIncreaseAmount;

    public event Action<int> OnScoreChanged = delegate { };

    private ScoreModel _model;

    public ScoreController()
    {
        _model = new ScoreModel();
        ResetCurrentScore();

        //_model.ResetHighScores(); //для тестовых целей, если вдруг надо обнулить очки.
    }

    public void ChangeCurrentScore(int amount)
    {
        _currentScore += amount;
        OnScoreChanged.Invoke(_currentScore);
    }

    public void IncreaseScoreByCurrentIncrement()
    {
        _currentScore += _currentScoreIncreaseAmount;
        OnScoreChanged.Invoke(_currentScore);
    }

    public void IncreaseScoreIncrement()
    {
        _currentScoreIncreaseAmount += Main.Instance.Settings.ScoreCountingSettings.ScoreDifficultyIncrement;
    }

    public void ResetCurrentScore()
    {
        _currentScore = 0;
        _currentScoreIncreaseAmount = Main.Instance.Settings.ScoreCountingSettings.InitialScorePerSuccess;
        OnScoreChanged.Invoke(_currentScore);
    }

    public int[] GetHighScoreRankings()
    {
        return _model.GetHighScores();
    }

    public void SaveNewHighScore()
    {
        int[] highScores = GetHighScoreRankings();
        _currentScoreRank = highScores.Length;

        for (int i = highScores.Length - 1; i >= 0; i--)
        {
            //сравниваем текущий счёт с элементом из таблицы рекордов
            if (_currentScore > highScores[i])
            {
                _currentScoreRank = i;
            }

            //если не попали в таблицу, выходим - таблица не изменилась, сохранять ничего не нужно
            if (_currentScoreRank == highScores.Length)
            {
                return;
            }
        }

        //если попали в таблицу, сдвигаем массив от нового значения
        for (int i = highScores.Length - 1; i > _currentScoreRank; i--)
        {
            highScores[i] = highScores[i - 1];
        }

        //записываем в массив новое значение
        highScores[_currentScoreRank] = _currentScore;

        //сохраняем
        _model.SaveHighScores(highScores);
    }
}
