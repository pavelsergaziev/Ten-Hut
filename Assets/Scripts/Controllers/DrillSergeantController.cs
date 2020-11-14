using System;
using UnityEngine;

using LanguagesAndTexts;
using OrdersAndExecution;

public class DrillSergeantController : IDependencyInjectionReceiver
{
    private SergeantSpeechController _sergeantSpeech;
    private SergeantAnimationController _sergeantAnimation;
    private LanguageDependentTextsController _textsController;
    private OrdersTexts _texts;

    private string _orderText;

    public event Action OnStoppedTalking = delegate { };

    public DrillSergeantController()
    {
        Main.Instance.SubscribeToDependencyInjection(this);
        _texts = new OrdersTexts();
    }

    public void InjectDependencies()
    {
        _textsController = Main.Instance.LanguageDependentTextsController;
        _sergeantSpeech = Main.Instance.SergeantSpeechController;
        _sergeantAnimation = Main.Instance.SergeantAnimationController;

        _textsController.OnTextsChanged += ChangeOrdersTexts;
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
        switch (orderData.Order)
        {
            case OrdersAndActions.none:
                break;
            case OrdersAndActions.wrong:
                break;
            case OrdersAndActions.oneTooMany:
                break;
            case OrdersAndActions.ten:
                _orderText = _texts.TenOrder;
                break;
            case OrdersAndActions.hut:
                _orderText = _texts.HutOrder;
                break;
            case OrdersAndActions.left:
                _orderText = _texts.LeftOrder;
                break;
            case OrdersAndActions.right:
                _orderText = _texts.RightOrder;
                break;
            default:
                break;
        }

        _sergeantSpeech.Say(_orderText, orderData.OrderAudioClips.AudioClips);
        _sergeantAnimation.LaunchOrderAnimation(orderData);

        _sergeantSpeech.OnSergeantSingleLineAudioEnded += ResolveAudioClipEnded;
    }

    public void SayAndAnimate(AudioClipsArraySO audioClipsArray)
    {
        _sergeantSpeech.Say(_texts.AtteeenOrder, audioClipsArray.AudioClips);
        _sergeantAnimation.LaunchNonOrderAnimation();

        _sergeantSpeech.OnSergeantSingleLineAudioEnded += ResolveAudioClipEnded;
    }

    private void ResolveAudioClipEnded()
    {
        _sergeantSpeech.OnSergeantSingleLineAudioEnded -= ResolveAudioClipEnded;
        OnStoppedTalking.Invoke();
    }

    private void ChangeOrdersTexts(Texts texts, TMPro.TMP_FontAsset font)
    {
        _texts.AtteeenOrder = Main.Instance.LanguageDependentTextsController.Texts.AtteeenOrder;
        _texts.TenOrder = Main.Instance.LanguageDependentTextsController.Texts.TenOrder;
        _texts.HutOrder = Main.Instance.LanguageDependentTextsController.Texts.HutOrder;
        _texts.LeftOrder = Main.Instance.LanguageDependentTextsController.Texts.LeftOrder;
        _texts.RightOrder = Main.Instance.LanguageDependentTextsController.Texts.RightOrder;
    }

    private class OrdersTexts
    {
        public string TenOrder;
        public string HutOrder;
        public string LeftOrder;
        public string RightOrder;
        public string AtteeenOrder;
    }
}

