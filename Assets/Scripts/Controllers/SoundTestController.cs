using System;
using UnityEngine;

using OrdersAndExecution;

public class SoundTestController : IDependencyInjectionReceiver
{
    private UIButtonEffectsController _buttonEffectsSource;

    //тут можно было и в диспетчер ткнуться, но хотелось большего единообразия, а диспетчера для сержанта у меня нет.
    private AudioFXPlayerController _audioFXTarget, _audioSergeantTarget;

    private AudioClip _audioFXSound, _sergeantSound;

    public SoundTestController()
    {
        Main.Instance.SubscribeToDependencyInjection(this);

        _audioFXSound = Main.Instance.Settings.AudioTestSoundsSO.AudioFX.AudioClips[0];
        _sergeantSound = Main.Instance.Settings.AudioTestSoundsSO.SergeantAudio.AudioClips[0];
    }

    public void InjectDependencies()
    {
        _buttonEffectsSource = Main.Instance.UIButtonEffectsController;
        _audioFXTarget = Main.Instance.CommonAudioFXController;
        _audioSergeantTarget = Main.Instance.SergeantAudioFXController;

        _buttonEffectsSource.OnButtonPressed += PlaySounds;
    }

    private void PlaySounds(MenuButtons button)
    {
        if (button != MenuButtons.TestSound)
            return;

        _audioFXTarget.OnAudioClipEnded += PlayFirstSound;
        
    }

    private void PlayFirstSound()
    {
        _audioFXTarget.OnAudioClipEnded -= PlayFirstSound;
        _audioFXTarget.PlayAudioClipReturnController(_audioFXSound, 1).OnAudioClipEnded += PlaySecondSound;
    }

    private void PlaySecondSound()
    {
        _audioFXTarget.OnAudioClipEnded -= PlaySecondSound;
        _audioSergeantTarget.PlayAudioClipReturnController(_sergeantSound, 1);

        //тут ещё можно было бы блокировать все остальные действия, пока тест идёт
        //(а то нажатие других кнопок вклинивается в подписки и это выглядит странно, хоть и работает)
        //, ну да ладно
    }
}
