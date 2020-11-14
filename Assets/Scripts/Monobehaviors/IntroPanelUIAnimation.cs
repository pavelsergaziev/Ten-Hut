using UnityEngine;

using DG.Tweening;

public class IntroPanelUIAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Sequence _scalingAnimation;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _scalingAnimation = DOTween.Sequence();
    }

    private void Start()
    {
        #region shaking animation
        _rectTransform.DOShakePosition
            (
                Random.Range
                (
                    Main.Instance.Settings.IntroPanelAnimationSO.MinShakeDuration,
                    Main.Instance.Settings.IntroPanelAnimationSO.MaxShakeDuration
                ),
                Main.Instance.Settings.IntroPanelAnimationSO.ShakeStrength,
                Main.Instance.Settings.IntroPanelAnimationSO.ShakeVibrato,
                Main.Instance.Settings.IntroPanelAnimationSO.ShakeRandomness,
                false,
                false
            )
        .SetLoops(-1, LoopType.Yoyo).timeScale = Main.Instance.Settings.IntroPanelAnimationSO.ShakeTimeScale;

        #endregion

        #region scaling animation

        float randomScaleDuration = Random.Range
                            (
                                Main.Instance.Settings.IntroPanelAnimationSO.MinScaleDuration,
                                Main.Instance.Settings.IntroPanelAnimationSO.MaxScaleDuration
                            );

        _scalingAnimation = DOTween.Sequence();

        _scalingAnimation.Append
            (
                _rectTransform.DOScale
                (
                    new Vector3
                    (
                        Main.Instance.Settings.IntroPanelAnimationSO.MinScaleSize,
                        Main.Instance.Settings.IntroPanelAnimationSO.MinScaleSize
                    ),
                    randomScaleDuration
                )
            );

        _scalingAnimation.Append(_rectTransform.DOScale(new Vector3(1,1), randomScaleDuration));

        _scalingAnimation.SetLoops(-1).Play();

        #endregion
    }

    private void OnDestroy()
    {
        //DOKill походу убивает только базовые твины, а не сиквенсы, поэтому их надо грохать отдельно.
        _scalingAnimation.Kill();
        _rectTransform.DOKill();
    }
}
