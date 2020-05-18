using UnityEngine;

using OrdersAndExecution;


[CreateAssetMenu(fileName = "OrdersAndExecutionSettings", menuName = "GameScriptableObjectsAsset/OrdersAndExecutionSettings")]
public class OrdersAndExecutionSettingsSO : ScriptableObject
{
    [SerializeField]
    private OrderSO[] _orders;

    [SerializeField] private float _initialDelayBetweenOrders, _difficultyDelayBetweenOrdersIncrement;
    [SerializeField] private int _initialOrdersInSequenceCount, _difficultyOrdersInSequenceIncrementMin, _difficultyOrdersInSequenceIncrementMax;
    [SerializeField] private int _attenIntermissionProbabilityIncrement;

    [SerializeField] private float _initialVoiceSpeed, _maxVoicePitchDeviation, _difficultyVoiceSpeedIncrement;

    [SerializeField] private AudioClipsArraySO _attenIntermissionsAudioClips;    

    [SerializeField] private int _amountOfWrongMovesToEndGame;



    public OrderSO[] Orders { get { return _orders; }}

    public float InitialDelayBetweenOrders { get { return _initialDelayBetweenOrders; } }
    public float DifficultyDelayBetweenOrdersIncrement { get { return _difficultyDelayBetweenOrdersIncrement; }}
    public int InitialOrdersInSequenceCount { get { return _initialOrdersInSequenceCount; }}
    public int DifficultyOrdersInSequenceIncrementMin { get { return _difficultyOrdersInSequenceIncrementMin; }}
    public int DifficultyOrdersInSequenceIncrementMax { get { return _difficultyOrdersInSequenceIncrementMax; } }
    public int AttenIntermissionProbabilityIncrement { get { return _attenIntermissionProbabilityIncrement; } }
    public AudioClipsArraySO AttenIntermissionsAudioClips { get { return _attenIntermissionsAudioClips; } }
    public int AmountOfWrongMovesToEndGame { get { return _amountOfWrongMovesToEndGame; } }
    public float InitialVoiceSpeed { get { return _initialVoiceSpeed; } }
    public float DifficultyVoiceSpeedIncrement { get { return _difficultyVoiceSpeedIncrement; } }
    public float MaxVoicePitchDeviation { get { return _maxVoicePitchDeviation; } }
}
