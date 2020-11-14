using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : IDependencyInjectionReceiver
{
    public event Action<AudioMixerGroups, float> OnInitialVolumeLoaded;
    public event Action<AudioMixerGroups, float> OnVolumeChanged;

    private OptionsInitialStateController _optionsSaver;

    private AudioMixerVolumeChanger _volumeChanger;

    private float _volumeChangeIncrement, _tempVolume;

    public AudioMixerController()
    {
        Main.Instance.SubscribeToDependencyInjection(this);
    }

    public void InjectDependencies()
    {
        _optionsSaver = Main.Instance.OptionsInitialStateController;

        //чтобы СНАЧАЛА уменьшилась громкость, а ПОТОМ проигрался звук нажатия кнопки, подписываемся на OnButtonPressedEarly
        //И-и-и это всё равно не помогает... Громкость не успевает измениться?
        Main.Instance.UIButtonEffectsController.OnButtonPressedEarly += ChangeVolume;        
    }

    public void SetInitialAudioMixerSettings()
    {
        _volumeChangeIncrement = Main.Instance.Settings.AudioMixersAllSettingsSO.AudioMixersOptionsParametersSettings.VolumeChangeIncrement;
        _volumeChanger = new AudioMixerVolumeChanger
            (
                Main.Instance.Settings.AudioMixersAllSettingsSO.AudioMixerSO.AudioMixerMaster,
                Main.Instance.Settings.AudioMixersAllSettingsSO.AudioMixersOptionsParametersSettings.MaxVolume
            );

        _volumeChanger.SetMainMixerVolume(Main.Instance.Settings.AudioMixersAllSettingsSO.AudioMixerSettingsOnFirstLaunchSO.MasterVolume);
        

        foreach (AudioMixerGroups audioMixerGroup in Enum.GetValues(typeof(AudioMixerGroups)))
        {
            _tempVolume = _optionsSaver.GetInitialVolume(audioMixerGroup);
            _volumeChanger.SetVolume(GetAudioMixerGroupByEnum(audioMixerGroup), _tempVolume);
            OnInitialVolumeLoaded?.Invoke(audioMixerGroup, _tempVolume);
        }
    }

    public float GetAudioMixerGroupVolume(AudioMixerGroups mixerGroup)
    {
        return _volumeChanger.GetAudioMixerGroupVolume(GetAudioMixerGroupByEnum(mixerGroup));
    }

    private void ChangeVolume(MenuButtons buttonPressed)
    {
        if (buttonPressed == MenuButtons.DecreaseFXVolume)
        {
            ChangeVolume(AudioMixerGroups.SoundFX, -_volumeChangeIncrement);
            return;
        }

        if (buttonPressed == MenuButtons.IncreaseFXVolume)
        {
            ChangeVolume(AudioMixerGroups.SoundFX, _volumeChangeIncrement);
            return;
        }

        if (buttonPressed == MenuButtons.DecreaseSpeechVolume)
        {
            ChangeVolume(AudioMixerGroups.SergeantSpeech, -_volumeChangeIncrement);
            return;
        }

        if (buttonPressed == MenuButtons.IncreaseSpeechVolume)
        {
            ChangeVolume(AudioMixerGroups.SergeantSpeech, _volumeChangeIncrement);
            return;
        }
    }

    private AudioMixerGroup GetAudioMixerGroupByEnum(AudioMixerGroups audioMixerGroup)
    {
        return Main.Instance.Settings.AudioMixersAllSettingsSO.AudioMixersDictionarySO.GetValueByKey(audioMixerGroup);
    }

    private void ChangeVolume(AudioMixerGroups audioMixerGroup, float amount)
    {
        _tempVolume = _volumeChanger.ChangeVolume(GetAudioMixerGroupByEnum(audioMixerGroup), amount);
        SaveVolumeToOptions(audioMixerGroup, _tempVolume);
        OnVolumeChanged?.Invoke(audioMixerGroup, _tempVolume);
    }

    private void SaveVolumeToOptions(AudioMixerGroups audioMixerGroup, float volume)
    {
        _optionsSaver.SetInitialVolume(audioMixerGroup, volume);
    }
}