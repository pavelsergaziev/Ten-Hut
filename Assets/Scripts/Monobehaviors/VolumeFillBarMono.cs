using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeFillBarMono : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroups _audioMixerGroup;

    [SerializeField]
    private Image _volumeBar;

    private AudioMixerController _audioMixerController;
    private float _volumeChangeIncrement;

    private void Start()
    {
        _audioMixerController = Main.Instance.AudioMixerController;
        _audioMixerController.OnInitialVolumeLoaded += ShowVolume;
        _audioMixerController.OnVolumeChanged += ShowVolume;

        _volumeChangeIncrement = Main.Instance.Settings.AudioMixersAllSettingsSO.AudioMixersOptionsParametersSettings.VolumeChangeIncrement;

        ShowVolume(_audioMixerGroup, _audioMixerController.GetAudioMixerGroupVolume(_audioMixerGroup));
    }

    private void ShowVolume(AudioMixerGroups audioMixerGroup, float volume)
    {
        if (audioMixerGroup == _audioMixerGroup)
        {
            _volumeBar.fillAmount = volume;
        }
    }

    private void OnDestroy()
    {
        _audioMixerController.OnInitialVolumeLoaded -= ShowVolume;
        _audioMixerController.OnVolumeChanged -= ShowVolume;
    }
}
