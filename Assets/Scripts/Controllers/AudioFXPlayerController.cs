using System;
using UnityEngine;

public class AudioFXPlayerController
{
    public event Action<AudioClip, float> OnAudioClipPlayRequest = delegate { };
    public event Action OnAudioClipEnded = delegate { };

    public void InvokeOnAudioClipEnded()
    {
        OnAudioClipEnded.Invoke();
    }

    public void PlayAudioClip(AudioClip audioClip, float pitch)
    {
        OnAudioClipPlayRequest.Invoke(audioClip, pitch);
    }
}
