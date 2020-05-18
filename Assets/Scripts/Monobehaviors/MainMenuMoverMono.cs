using UnityEngine;

using DG.Tweening;

public class MainMenuMoverMono : MonoBehaviour
{
    [SerializeField]
    private RectTransform _panelCredits, _panelHowToPlay, _panelHighScore;

    private UIButtonEffectsController _buttonEvents;
    private RectTransform _rectTransform;
    private Vector2 _targetPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _buttonEvents = Main.Instance.UIButtonEffectsController;
        _buttonEvents.OnButtonPressed += TempMove;

        PositionMenuPanels();
    }

    private void PositionMenuPanels()
    {
        Resolution _screenResolution = Screen.currentResolution;

        _panelHowToPlay.anchoredPosition = new Vector2
            (
                _screenResolution.width + (_panelCredits.rect.width/2),
                _panelHowToPlay.anchoredPosition.y
            );

        _panelHighScore.anchoredPosition = new Vector2
            (
                - _screenResolution.width - (_panelCredits.rect.width / 2),
                _panelHighScore.anchoredPosition.y
            );

        _panelCredits.anchoredPosition = new Vector2
            (
                _panelCredits.anchoredPosition.x,
                - _screenResolution.height - (_panelCredits.rect.height)
            );

    }

    private void TempMove(MenuButtons buttonPushed)
    {
        switch (buttonPushed)
        {
            case MenuButtons.StartGame:
                break;
            case MenuButtons.HowToPlay:
                LaunchMovementAnimation(_panelHowToPlay.anchoredPosition);                
                break;
            case MenuButtons.HighScore:
                LaunchMovementAnimation(_panelHighScore.anchoredPosition);
                break;
            case MenuButtons.Credits:
                LaunchMovementAnimation(_panelCredits.anchoredPosition);
                break;
            case MenuButtons.ExitGame:
                break;
            case MenuButtons.ReturnToPreviousScreen:
                LaunchMovementAnimation(Vector2.zero);
                break;
            default:
                break;
        }        
    }

    private void LaunchMovementAnimation(Vector2 targetPosition)
    {
        _rectTransform.DOAnchorPos
            (
                -targetPosition,
                Main.Instance.Settings.MenusAnimationSettings.MovementDuration
            );
    }

    private void OnDestroy()
    {
        _buttonEvents.OnButtonPressed -= TempMove;
    }
}
