using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SergeantSpeechController : IDependencyInjectionReceiver
{
    public event Action<string> OnSayLineRequested = delegate { };

    public event Action<string> OnSpeechLetterByLetterStarted = delegate { };
    public event Action OnSpeechLetterByLetterEnded = delegate { };

    public event Action OnSergeantSingleLineAudioEnded = delegate { };


    private string _currentText;
    private AudioClip[] _currentAudioClips;

    private int _previousClipIndex;//да, он часто будет делать лишнюю ненужную работу, но и почему бы нет...
    private int _nextCharIndex;

    private float _currentBasePitch;
    private float _maxPitchDeviation;

    private AudioFXPlayerController _audioController;

    public SergeantSpeechController()
    {
        Main.Instance.SubscribeToDependencyInjection(this);
        _maxPitchDeviation = Main.Instance.Settings.OrdersAndExecutionSettings.MaxVoicePitchDeviation;
        _currentBasePitch = Main.Instance.Settings.OrdersAndExecutionSettings.InitialVoiceSpeed;
    }

    public void InjectDependencies()
    {
        _audioController = Main.Instance.SergeantAudioFXController;
    }


    public void Say(string text, AudioClip[] audioClips, bool printLetterByLetter = false)
    {
        if (printLetterByLetter)
        {
            OnSpeechLetterByLetterStarted.Invoke(text);
            _currentText = text;
            _currentAudioClips = audioClips;
            PlayRandomAudioClip();

            _audioController.OnAudioClipEnded += PlayRandomAudioClip;
        }
        else
        {
            _audioController.PlayAudioClip(GetRandomAudioClip(audioClips), GetRandomPitch());
            OnSayLineRequested.Invoke(text);
            _audioController.OnAudioClipEnded += ResolveLineAudioClipEnded;
        }
    }

    public void IncreaseVoiceSpeed(float amount)
    {
        _currentBasePitch += amount;
    }

    public void SetVoiceSpeedTo(float amount)
    {
        _currentBasePitch = amount;
    }

    private float GetRandomPitch()
    {
        return Random.Range(_currentBasePitch - _maxPitchDeviation, _currentBasePitch + _maxPitchDeviation);
    }

    private void PlayRandomAudioClip()
    {
        _audioController.PlayAudioClip(GetRandomAudioClip(_currentAudioClips), GetRandomPitch());
    }

    public void EndLetterByLetterSpeech()
    {
        _audioController.OnAudioClipEnded -= PlayRandomAudioClip;
        OnSpeechLetterByLetterEnded.Invoke();
    }

    public void ResolveLineAudioClipEnded()
    {
        _audioController.OnAudioClipEnded -= ResolveLineAudioClipEnded;
        OnSergeantSingleLineAudioEnded.Invoke();
    }

    private AudioClip GetRandomAudioClip(AudioClip[] clipsArray)
    {
        int randomIndex = Random.Range(0, clipsArray.Length);

        if (randomIndex == _previousClipIndex)
        {
            randomIndex = randomIndex.ChangeArrayIndexRandomlyNoRepeat(clipsArray.Length);
        }
        
        _previousClipIndex = randomIndex;

        return clipsArray[randomIndex];
    }
}