using UnityEngine;

[CreateAssetMenu(fileName = "SoldierAnimations", menuName = "GameScriptableObjectsAsset/SoldierAnimations")]
public class SoldierAnimationsSO : ScriptableObject
{
    [SerializeField]
    private Sprite[] _positionsClockwise, _salutePositionsClockwise;

    [SerializeField]
    private float _tweenJumpPower, _tweenJumpDuration, _tweenScaleX, _tweenScaleY, _tweenScaleDuration, _tweenScaleElasticity;
    [SerializeField]
    private int _tweenScaleVibrato;

    public Sprite[] PositionsClockwise { get { return _positionsClockwise; } }
    public Sprite[] SalutePositionsClockwise { get { return _salutePositionsClockwise; } }

    public float TweenJumpPower { get { return _tweenJumpPower; } }
    public float TweenJumpDuration { get { return _tweenJumpDuration; } }
    public float TweenScaleX { get { return _tweenScaleX; } }
    public float TweenScaleY { get { return _tweenScaleY; } }
    public float TweenScaleDuration { get { return _tweenScaleDuration; } }
    public float TweenScaleElasticity { get { return _tweenScaleElasticity; } }
    public int TweenScaleVibrato { get { return _tweenScaleVibrato; } }
}
