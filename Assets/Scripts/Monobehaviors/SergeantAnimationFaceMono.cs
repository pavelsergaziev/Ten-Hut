using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using OrdersAndExecution;

public class SergeantAnimationFaceMono : MonoBehaviour
{
    [SerializeField]
    Image _spriteRenderer;

    private SergeantAnimationController _controller;

    private Sprite _idleSprite;
    private Sprite[] _animationSpritesSequenced;

    private bool _isAnimating;
    private Coroutine _animationCoroutine;
    private int _currentAnimationFrameIndex;
    private WaitForSeconds _animationFrameChangeWait;

    private void Start()
    {
        _controller = Main.Instance.SergeantAnimationController;
        _controller.OnOrderAnimationRequested += AnimateFace;
        _controller.OnNonOrderAnimationRequested += AnimateFace;

        _animationFrameChangeWait = new WaitForSeconds(Main.Instance.Settings.SergeantAnimation.AnimationFrameChangeDelay);
        _idleSprite = Main.Instance.Settings.SergeantAnimation.IdleSprite;
        _animationSpritesSequenced = Main.Instance.Settings.SergeantAnimation.AnimationSpritesSequenced;

        ResetSpriteToIdle();
    }

    private void AnimateFace(OrderSO order)
    {
        AnimateFace();        
    }

    private void AnimateFace()
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        ResetSpriteToIdle();

        while (_currentAnimationFrameIndex < _animationSpritesSequenced.Length)
        {
            _spriteRenderer.sprite = _animationSpritesSequenced[_currentAnimationFrameIndex++];
            yield return _animationFrameChangeWait;
        }

        _currentAnimationFrameIndex = _animationSpritesSequenced.Length - 1;

        while (_currentAnimationFrameIndex >= 0)
        {
            _spriteRenderer.sprite = _animationSpritesSequenced[_currentAnimationFrameIndex--];
            yield return _animationFrameChangeWait;
        }

        ResetSpriteToIdle();
    }

    private void ResetSpriteToIdle()
    {
        _currentAnimationFrameIndex = 0;
        _spriteRenderer.sprite = _idleSprite;
    }

    private void OnDestroy()
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);//излишне?
        }

        _controller.OnOrderAnimationRequested -= AnimateFace;
        _controller.OnNonOrderAnimationRequested -= AnimateFace;
    }
}
