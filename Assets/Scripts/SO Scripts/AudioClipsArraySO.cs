using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsArray", menuName = "GameScriptableObjectsAsset/AudioClipsArray")]
public class AudioClipsArraySO : ScriptableObject
{
    [SerializeField] private AudioClip[] _audioClips;
    public AudioClip[] AudioClips { get { return _audioClips; } }
}
