using UnityEngine;

[CreateAssetMenu(fileName = "AudioMixersAllSettingsSO", menuName = "GameScriptableObjectsAsset/AudioMixersAllSettingsSO")]
public class AudioMixersAllSettingsSO :  ScriptableObject
{
    [SerializeField]
    private AudioMixerSettingsOnFirstLaunchSO _audioMixerSettingsOnFirstLaunchSO;
    public AudioMixerSettingsOnFirstLaunchSO AudioMixerSettingsOnFirstLaunchSO { get { return _audioMixerSettingsOnFirstLaunchSO; } }

    [SerializeField]
    private AudioMixerSO _audioMixerSO;
    public AudioMixerSO AudioMixerSO { get { return _audioMixerSO; } }

    [SerializeField]
    private AudioMixersDictionarySO _audioMixersDictionarySO;
    public AudioMixersDictionarySO AudioMixersDictionarySO { get { return _audioMixersDictionarySO; } }

    [SerializeField]
    private AudioMixersOptionsParametersSettingsSO _audioMixersOptionsParametersSettings;
    public AudioMixersOptionsParametersSettingsSO AudioMixersOptionsParametersSettings { get { return _audioMixersOptionsParametersSettings; } }
}