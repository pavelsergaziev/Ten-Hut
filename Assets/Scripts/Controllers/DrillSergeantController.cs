using System;

using OrdersAndExecution;

public class DrillSergeantController : IDependencyInjectionReceiver
{
    private SergeantSpeechController _sergeantSpeech;
    private SergeantAnimationController _sergeantAnimation;

    public event Action OnStoppedTalking = delegate { };

    public DrillSergeantController()
    {
        Main.Instance.SubscribeToDependencyInjection(this);
    }

    public void InjectDependencies()
    {
        _sergeantSpeech = Main.Instance.SergeantSpeechController;
        _sergeantAnimation = Main.Instance.SergeantAnimationController;
    }

    public void IncreaseVoiceSpeedBy(float amount)
    {
        _sergeantSpeech.IncreaseVoiceSpeed(amount);
    }

    public void SetVoiceSpeedTo(float amount)
    {
        _sergeantSpeech.SetVoiceSpeedTo(amount);
    }


    public void SayAndAnimate(OrderSO orderData)
    {
        _sergeantSpeech.Say(orderData.OrderText, orderData.OrderAudioClips.AudioClips);
        _sergeantAnimation.LaunchOrderAnimation(orderData);

        _sergeantSpeech.OnSergeantSingleLineAudioEnded += ResolveAudioClipEnded;
    }

    public void SayAndAnimate(AudioClipsArraySO audioClipsArray)
    {
        _sergeantSpeech.Say("Atte-e-e-e-en...", audioClipsArray.AudioClips);
        _sergeantAnimation.LaunchNonOrderAnimation();

        _sergeantSpeech.OnSergeantSingleLineAudioEnded += ResolveAudioClipEnded;
    }

    private void ResolveAudioClipEnded()
    {
        _sergeantSpeech.OnSergeantSingleLineAudioEnded -= ResolveAudioClipEnded;
        OnStoppedTalking.Invoke();
    }
}