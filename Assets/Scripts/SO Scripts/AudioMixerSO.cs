using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioMixerSO", menuName = "GameScriptableObjectsAsset/AudioMixerSO")]
public class AudioMixerSO :  ScriptableObject
{
    [SerializeField]
    private AudioMixer _audioMixerMaster;
    public AudioMixer AudioMixerMaster { get { return _audioMixerMaster; } }
}