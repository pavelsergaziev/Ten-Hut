using UnityEngine;

[CreateAssetMenu(fileName = "AudioTestSoundsSO", menuName = "GameScriptableObjectsAsset/AudioTestSoundsSO")]
public class AudioTestSoundsSO :  ScriptableObject
{
    [SerializeField]
    private AudioClipsArraySO _audioFX;
    public AudioClipsArraySO AudioFX { get { return _audioFX; } }

    [SerializeField]
    private AudioClipsArraySO _sergeantAudio;
    public AudioClipsArraySO SergeantAudio { get { return _sergeantAudio; } }
}