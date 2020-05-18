using UnityEngine;

[CreateAssetMenu(fileName = "CommonAudioFXSettings", menuName = "GameScriptableObjectsAsset/CommonAudioFXSettings")]
public class CommonAudioFXSettingsSO : ScriptableObject
{
    [SerializeField] private AudioFXDictionarySO _audioFXDictionary;
    [SerializeField] private float _maxPitchDeviation;

    public AudioFXDictionarySO AudioFXDictionary { get { return _audioFXDictionary; } }
    public float MaxPitchDeviation { get { return _maxPitchDeviation; } }
}
