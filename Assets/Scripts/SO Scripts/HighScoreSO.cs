using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighScoreSettings", menuName = "GameScriptableObjectsAsset/HighScoreSettings")]
public class HighScoreSO : ScriptableObject
{
    [SerializeField] private int _highScoreSlotsCount;
    public int HighScoreSlotsCount { get { return _highScoreSlotsCount; } }
}
