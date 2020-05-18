using OrdersAndExecution;
using UnityEngine;
using DG.Tweening;

public class SoldierAnimationMono : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _soldierSprite;

    private SoldierAnimationController _controller;
    private Sprite[] _positions, _salutePositions;

    private int _currentPositionIndex;

    private Transform _transform;
    private Sequence _currentTweenSequence;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _positions = Main.Instance.Settings.SoldierAnimations.PositionsClockwise;
        _salutePositions = Main.Instance.Settings.SoldierAnimations.SalutePositionsClockwise;

        _controller = Main.Instance.SoldierAnimationController;
        _controller.OnPlayerActionExecuted += Animate;
        _controller.OnReturnToNonHutPositionRequested += ReturnToNonHutPosition;
        _controller.OnReturnToInitialPositionRequested += ReturnToInitialPosition;
    }

    private void Animate(OrdersAndActions playerAction)
    {
        switch (playerAction)
        {
            case OrdersAndActions.none:
                break;
            case OrdersAndActions.hut:
                _soldierSprite.sprite = _salutePositions[_currentPositionIndex];
                break;
            case OrdersAndActions.left:
                SwitchToNextPosition(-1);
                break;
            case OrdersAndActions.right:
                SwitchToNextPosition(1);
                break;
            case OrdersAndActions.wrong:
                break;
            case OrdersAndActions.oneTooMany:
                break;
            case OrdersAndActions.ten:
                break;
            default:
                break;
        }

        Tween();
    }

    private void Tween()
    {
        if (_currentTweenSequence != null)
        {
            _currentTweenSequence.Complete();
        }
        
        _currentTweenSequence = _transform.DOJump
            (
                _transform.position,
                Main.Instance.Settings.SoldierAnimations.TweenJumpPower,
                1,
                Main.Instance.Settings.SoldierAnimations.TweenJumpDuration
            );

        _currentTweenSequence.Insert(0,_transform.DOPunchScale
            (
                new Vector3
                (
                    Main.Instance.Settings.SoldierAnimations.TweenScaleX,
                    Main.Instance.Settings.SoldierAnimations.TweenScaleY,
                    1
                 ),
                 Main.Instance.Settings.SoldierAnimations.TweenScaleDuration,
                 Main.Instance.Settings.SoldierAnimations.TweenScaleVibrato,
                 Main.Instance.Settings.SoldierAnimations.TweenScaleElasticity
             ));        
    }

    private void ReturnToNonHutPosition()
    {
        if (_soldierSprite.sprite == _salutePositions[_currentPositionIndex])
        {
            _soldierSprite.sprite = _positions[_currentPositionIndex];
        }
    }


    private void ReturnToInitialPosition()
    {
        _currentPositionIndex = 0;
        _soldierSprite.sprite = _positions[_currentPositionIndex];
    }

    private void SwitchToNextPosition(int moveValue)
    {
        _currentPositionIndex = _currentPositionIndex.ChangeClosedLoopArrayIndex(_positions.Length, moveValue);
        _soldierSprite.sprite = _positions[_currentPositionIndex];
    }

    private void OnDestroy()
    {
        _controller.OnPlayerActionExecuted -= Animate;
        _controller.OnReturnToNonHutPositionRequested -= ReturnToNonHutPosition;
        _controller.OnReturnToInitialPositionRequested -= ReturnToInitialPosition;
    }
}
