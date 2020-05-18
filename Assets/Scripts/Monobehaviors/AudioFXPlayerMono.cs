using UnityEngine;

public class AudioFXPlayerMono : AudioPlayerMono_Base
{
    protected override void SetController()
    {
        _audioController = Main.Instance.CommonAudioFXController;
    }
}
