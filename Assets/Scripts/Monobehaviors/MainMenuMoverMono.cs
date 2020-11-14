using UnityEngine;

using DG.Tweening;

public class MainMenuMoverMono : MonoBehaviour
{
    [SerializeField]
    private RectTransform _panelMainMenu, _panelCredits, _panelHowToPlay, _panelHighScore, _panelOptions;

    private UIButtonEffectsController _buttonEvents;
    private RectTransform _rectTransform;
    private Vector2 _targetPosition;
    private float _movementDuration;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _movementDuration = Main.Instance.Settings.MenusAnimationSettings.MovementDuration;

        _buttonEvents = Main.Instance.UIButtonEffectsController;
        _buttonEvents.OnButtonPressed += SwitchPanels;

        PositionMenuPanels();

        _panelCredits.gameObject.SetActive(false);
        _panelHighScore.gameObject.SetActive(false);
        _panelHowToPlay.gameObject.SetActive(false);
        _panelOptions.gameObject.SetActive(false);
        _panelMainMenu.gameObject.SetActive(true);
    }

    private void PositionMenuPanels()
    {
        Resolution _screenResolution = Screen.currentResolution;

        _panelHowToPlay.anchoredPosition = new Vector2
            (
                _screenResolution.width + (_panelCredits.rect.width / 2),
                _panelHowToPlay.anchoredPosition.y
            );

        _panelHighScore.anchoredPosition = new Vector2
            (
                -_screenResolution.width - (_panelCredits.rect.width / 2),
                _panelHighScore.anchoredPosition.y
            );

        _panelCredits.anchoredPosition = new Vector2
            (
                _panelCredits.anchoredPosition.x,
                -_screenResolution.height - (_panelCredits.rect.height)
            );

        _panelOptions.anchoredPosition = new Vector2
            (
                _panelOptions.anchoredPosition.x,
                _screenResolution.height + (_panelOptions.rect.height)
             );

    }

    private void SwitchPanels(MenuButtons buttonPushed)
    {
        switch (buttonPushed)
        {
            case MenuButtons.StartGame:
                break;
            case MenuButtons.HowToPlay:
                SwitchPanels(_panelHowToPlay, _panelMainMenu);
                break;
            case MenuButtons.Options:
                SwitchPanels(_panelOptions, _panelMainMenu);
                break;
            case MenuButtons.HighScore:
                SwitchPanels(_panelHighScore, _panelMainMenu);
                break;
            case MenuButtons.Credits:
                SwitchPanels(_panelCredits, _panelMainMenu);
                break;
            case MenuButtons.ExitGame:
                break;
            case MenuButtons.ReturnToMainMenu:
                SwitchPanels(_panelMainMenu);
                break;
            default:
                break;
        }
    }

    private void SwitchPanels(RectTransform to, RectTransform from = null)
    {
        to.gameObject.SetActive(true);
        _rectTransform.DOAnchorPos(-to.anchoredPosition, _movementDuration)
            .OnComplete(() => DeactivatePanel_AllButMainMenuIfNull(from));
    }

    private void DeactivatePanel_AllButMainMenuIfNull(RectTransform panel)
    {
        if (panel == null)
        {
            _panelCredits.gameObject.SetActive(false);
            _panelHowToPlay.gameObject.SetActive(false);
            _panelHighScore.gameObject.SetActive(false);
            _panelOptions.gameObject.SetActive(false);
        }
        else
        {
            panel.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _buttonEvents.OnButtonPressed -= SwitchPanels;
    }
}
