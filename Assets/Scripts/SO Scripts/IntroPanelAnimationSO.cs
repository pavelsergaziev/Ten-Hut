using UnityEngine;

[CreateAssetMenu(fileName = "IntroPanelAnimationSO", menuName = "GameScriptableObjectsAsset/IntroPanelAnimationSO")]
public class IntroPanelAnimationSO :  ScriptableObject
{

    [SerializeField]
    private int _shakeVibrato;

    [SerializeField]
    private float _minShakeDuration, _maxShakeDuration,
        _shakeStrength, _shakeRandomness, _shakeTimeScale,
        _minScaleSize, _minScaleDuration, _maxScaleDuration;

    public float MinShakeDuration { get { return _minShakeDuration; } }
    public float MaxShakeDuration { get { return _maxShakeDuration; } }
    public float ShakeStrength { get { return _shakeStrength; } }
    public int ShakeVibrato { get { return _shakeVibrato; } }
    public float ShakeRandomness { get { return _shakeRandomness; } }
    public float ShakeTimeScale { get { return _shakeTimeScale; } }
    public float MinScaleSize { get { return _minScaleSize; } }
    public float MinScaleDuration { get { return _minScaleDuration; } }
    public float MaxScaleDuration { get { return _maxScaleDuration; } }

}