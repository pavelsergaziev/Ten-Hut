using UnityEngine;
using DG.Tweening;

public class UIShakerMono : MonoBehaviour
{
    private RectTransform _rectTransform;

    private PlayerHealthController _healthController;

    public void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        _healthController = Main.Instance.PlayerHealthController;
        _healthController.OnCurrentHealthChanged += Shake;
    }

    private void Shake(int currentHealth)
    {
        if (currentHealth < _healthController.MaxHealth)
        {
            _rectTransform.DOShakePosition
                (
                    Main.Instance.Settings.UIShakeSettings.Duration,
                    Main.Instance.Settings.UIShakeSettings.Strength
                );
        }
    }

    private void OnDestroy()
    {
        _healthController.OnCurrentHealthChanged -= Shake;
    }
}
