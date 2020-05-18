using UnityEngine;

public class AudioPlayerSergeantMono : AudioPlayerMono_Base
{
    protected override void SetController()
    {
        _audioController = Main.Instance.SergeantAudioFXController;
    }
}
