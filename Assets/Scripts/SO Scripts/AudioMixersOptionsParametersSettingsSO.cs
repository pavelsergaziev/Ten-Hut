using UnityEngine;

[CreateAssetMenu(fileName = "AudioMixersOptionsParametersSettingsSO", menuName = "GameScriptableObjectsAsset/AudioMixersOptionsParametersSettingsSO")]
public class AudioMixersOptionsParametersSettingsSO :  ScriptableObject
{
    [SerializeField]
    private float _maxVolume;
    public float MaxVolume { get { return _maxVolume; } }

    [SerializeField]
    private float _volumeChangeIncrement;
    public float VolumeChangeIncrement { get { return _volumeChangeIncrement; } }
}