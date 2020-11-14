using System;
using UnityEngine;

public class AudioFXDispatcherController: IDependencyInjectionReceiver
{
    private AudioFXPlayerController _audioFXController;
    AudioClip[] _audioClips;
    private int _currentAudioClipIndex;
    private AudioFXType _previousAudioFXType;

    private PlayerHealthController _playerHealth;
    private UIButtonEffectsController _buttons;
    private SoldierAnimationController _soldierAnimation;

    public AudioFXDispatcherController()
    {
        _previousAudioFXType = AudioFXType.none;
        Main.Instance.SubscribeToDependencyInjection(this);
    }

    public void InjectDependencies()
    {
        _audioFXController = Main.Instance.CommonAudioFXController;
        _playerHealth = Main.Instance.PlayerHealthController;
        _buttons = Main.Instance.UIButtonEffectsController;
        _soldierAnimation = Main.Instance.SoldierAnimationController;

        _playerHealth.OnCurrentHealthChanged += Play;
        _buttons.OnButtonPressed += Play;
        _soldierAnimation.OnPlayerActionExecuted += Play;
    }

    private void Play(OrdersAndExecution.OrdersAndActions playerAction)
    {
        PlayFX(AudioFXType.soldierMove);
    }

    private void Play(MenuButtons button)
    {
        PlayFX(AudioFXType.buttonClick);
    }

    private void Play(int currentHealth)
    {
        if (currentHealth < _playerHealth.MaxHealth)
        {
            PlayFX(AudioFXType.healthHit);
        }
    }

    public void PlayFX(AudioFXType audioFXtype)
    {
        float pitch = UnityEngine.Random.Range
            (
                1 - Main.Instance.Settings.CommonAudioFXSettings.MaxPitchDeviation,
                1 + Main.Instance.Settings.CommonAudioFXSettings.MaxPitchDeviation
            );

        _audioClips = Main.Instance.Settings.CommonAudioFXSettings.AudioFXDictionary
            .GetValueByKey(audioFXtype).AudioClips;

        if (audioFXtype == _previousAudioFXType)
        {
            _currentAudioClipIndex = _currentAudioClipIndex.ChangeArrayIndexRandomlyNoRepeat(_audioClips.Length);
        }
        else
        {
            _currentAudioClipIndex = UnityEngine.Random.Range(0, _audioClips.Length);
            _previousAudioFXType = audioFXtype;
        }

        _audioFXController.PlayAudioClip(_audioClips[_currentAudioClipIndex], pitch);
    }
}
