using UnityEngine;

[CreateAssetMenu(fileName = "AudioMixerSettingsOnFirstLaunchSO", menuName = "GameScriptableObjectsAsset/AudioMixerSettingsOnFirstLaunchSO")]
public class AudioMixerSettingsOnFirstLaunchSO :  ScriptableObject
{
    [SerializeField]
    private float _masterVolume;
    public float MasterVolume { get { return _masterVolume; } }

    [SerializeField]
    private AudioMixerGroupsSettingsOnFirstLaunchDictionarySO _mixerGroupsVolumes;
    public AudioMixerGroupsSettingsOnFirstLaunchDictionarySO MixerGroupsVolumes { get { return _mixerGroupsVolumes; } }
}