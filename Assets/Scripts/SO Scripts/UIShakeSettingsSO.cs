using UnityEngine;

[CreateAssetMenu(fileName = "UIShakeSettings", menuName = "GameScriptableObjectsAsset/UIShakeSettings")]
public class UIShakeSettingsSO : ScriptableObject
{
    [SerializeField] private float _duration, _strength;

    public float Duration { get { return _duration; } }
    public float Strength { get { return _strength; } }
}
