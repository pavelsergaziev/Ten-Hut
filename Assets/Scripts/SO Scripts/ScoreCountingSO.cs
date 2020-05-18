using UnityEngine;

[CreateAssetMenu(fileName = "ScoreCountingSettings", menuName = "GameScriptableObjectsAsset/ScoreCountingSettings")]
public class ScoreCountingSO : ScriptableObject
{
    [SerializeField] private int _initialScorePerSuccess;
    public int InitialScorePerSuccess { get { return _initialScorePerSuccess; } }

    [SerializeField] private int _scoreIncrement;
    public int ScoreDifficultyIncrement { get { return _scoreIncrement; } }
}