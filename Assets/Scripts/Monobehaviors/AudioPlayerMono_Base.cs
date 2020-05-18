using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioPlayerMono_Base : MonoBehaviour, IUpdated
{
    private AudioSource _audioSource;
    protected AudioFXPlayerController _audioController;

    private bool _isPlaying;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Main.Instance.Updater.Subscribe(this);

        SetController();

        _audioController.OnAudioClipPlayRequest += PlayAudioClip;
    }

    protected virtual void SetController()
    {
        throw new NotImplementedException();
    }

    public void PlayAudioClip(AudioClip clip, float pitch)
    {
        _isPlaying = true;

        _audioSource.pitch = pitch;
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void Tick()
    {
        if (!_isPlaying)
            return;

        if (_audioSource != null && !_audioSource.isPlaying)
        {
            _isPlaying = false;
            _audioController.InvokeOnAudioClipEnded();
        }
    }

    private void OnDestroy()
    {
        Main.Instance.Updater.Unsubscribe(this);
        _audioController.OnAudioClipPlayRequest -= PlayAudioClip;
    }
}
