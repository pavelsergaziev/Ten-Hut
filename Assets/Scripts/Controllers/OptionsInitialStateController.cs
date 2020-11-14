using System;
using UnityEngine;
using LanguagesAndTexts;

public class OptionsInitialStateController
{
    private OptionsSavingModel _model;

    public OptionsInitialStateController()
    {
        _model = new OptionsSavingModel();
    }

    public Languages GetInitialLanguage()
    {
        return (Languages)_model.GetInitialLanguageId();
    }

    public void SetInitialLanguage(Languages language)
    {
        _model.SetInitialLanguage((int)language);
    }

    public float GetInitialVolume(AudioMixerGroups mixerGroup)
    {
        switch (mixerGroup)
        {
            case AudioMixerGroups.SoundFX:
                return _model.GetInitialFXVolume();
            case AudioMixerGroups.SergeantSpeech:
                return _model.GetInitialSergeantSpeechVolume();
            default:
                return default;
        }
    }

    public void SetInitialVolume(AudioMixerGroups mixerGroup, float volume)
    {
        switch (mixerGroup)
        {
            case AudioMixerGroups.SoundFX:
                _model.SetInitialFXVolume(volume);
                break;
            case AudioMixerGroups.SergeantSpeech:
                _model.SetInitialSergeantSpeechVolume(volume);
                break;
            default:
                break;
        }
    }

    public float GetInitialFXVolume()
    {
        return _model.GetInitialFXVolume();
    }

    public void SetInitialFXVolume(int volume)
    {
        _model.SetInitialFXVolume(volume);
    }

    public float GetInitialSergeantSpeechVolume()
    {
        return _model.GetInitialSergeantSpeechVolume();
    }

    public void SetInitialSergeantSpeechVolume(int volume)
    {
        _model.SetInitialSergeantSpeechVolume(volume);
    }

    public void SetValuesFromSOIfFirstLaunch()
    {
        _model.IfFirstLaunchThenSetInitialValues
            (
                (int)Languages.English,//переделать в взять из SO
                Main.Instance.Settings.AudioMixersAllSettingsSO.AudioMixerSettingsOnFirstLaunchSO.MixerGroupsVolumes.GetValueByKey(AudioMixerGroups.SoundFX),
                Main.Instance.Settings.AudioMixersAllSettingsSO.AudioMixerSettingsOnFirstLaunchSO.MixerGroupsVolumes.GetValueByKey(AudioMixerGroups.SergeantSpeech)
            );
    }
}
