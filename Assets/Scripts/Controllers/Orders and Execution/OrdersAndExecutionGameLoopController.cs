using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OrdersAndExecution
{
    public class OrdersAndExecutionGameLoopController : IUpdated, IDependencyInjectionReceiver
    {
        public event Action OnGameOver;

        private float _initialDelayBetweenOrders, _difficultyDelayBetweenOrdersIncrement;
        private int _initialOrdersInSequenceCount, _difficultyOrdersInSequenceIncrement;

        private float _timer, _timerMaxValue;

        private int _attenIntermissionCurrentProbability;

        private OrderSO _currentOrder;
        private OrdersAndActions _currentPlayerAction;

        private Updater _updater;
        private PlayerActionsInputController _playerInput;
        private SoldierAnimationController _soldierAnimation;
        private DrillSergeantController _sergeant;
        private ScoreController _score;

        private PlayerHealthController _playerHealth;
        private OrdersSequence _ordersSequence;

        
        private enum State { none, inAttenIntermission, orderAudioIsPlaying, timerIsTicking, gameOver, notRunning }
        private State _state;

        private bool _playerHasAlreadyActed;
        private bool _isFirstSequence;
        

        public OrdersAndExecutionGameLoopController()
        {
            Main.Instance.SubscribeToDependencyInjection(this);

            _timerMaxValue = Main.Instance.Settings.OrdersAndExecutionSettings.InitialDelayBetweenOrders;

            _ordersSequence = new OrdersSequence(Main.Instance.Settings.OrdersAndExecutionSettings.Orders, Main.Instance.Settings.OrdersAndExecutionSettings.InitialOrdersInSequenceCount);
            

            _state = new State();
            _state = State.notRunning;
        }

        public void InjectDependencies()
        {
            _updater = Main.Instance.Updater;
            _updater.Subscribe(this);

            _sergeant = Main.Instance.DrillSergeantController;
            _playerInput = Main.Instance.PlayerActionsController;

            _score = Main.Instance.ScoreController;
            _playerHealth = Main.Instance.PlayerHealthController;
            _soldierAnimation = Main.Instance.SoldierAnimationController;
        }

        public void LaunchGame()
        {
            _isFirstSequence = true;
            _state = State.none;
            ResetPlayerAction();
            _playerHealth.ResetHealth();
            ResetDifficulty();
            ResetSoldierPosition();

            _timer = 0;
        }

        public void Tick()
        {
            if (_state == State.gameOver || _state == State.notRunning)
                return;

            if (_state == State.inAttenIntermission || _state == State.orderAudioIsPlaying)
            {
                CheckPlayerAction();
                return;
            }

            _timer -= Time.deltaTime;

            if (_timer > 0)
            {
                CheckPlayerAction();                
                return;
            }



            //если таймер на нуле

            CheckOrderExecutedOnTimedOut();            

            if (_state == State.gameOver)
                return;

            if (_ordersSequence.IsSequenceEmpty())
            {
                if (_isFirstSequence)
                {
                    _isFirstSequence = false;
                }
                else
                {
                    IncreaseDifficulty();
                }

                _ordersSequence.CreateNewSequence();
                _attenIntermissionCurrentProbability = 100;
            }

            if (IsTimeForAttenIntermission())
            {
                LaunchAttenIntermissionAndDecreaseNextIntermissionProbability();
            }
            else
            {
                IssueOrder();
            }

            IncreaseAttenIntermissionProbability();
            ResetPlayerAction();
        }

        private void IncreaseAttenIntermissionProbability()
        {
            _attenIntermissionCurrentProbability += Main.Instance.Settings.OrdersAndExecutionSettings.AttenIntermissionProbabilityIncrement;
        }

        public void EndIntermissionAndIssueNextOrder()
        {
            _sergeant.OnStoppedTalking -= EndIntermissionAndIssueNextOrder;
            IssueOrder();
        }

        private bool IsTimeForAttenIntermission()
        {
            return Random.Range(0, 100) < _attenIntermissionCurrentProbability;
        }

        private void LaunchAttenIntermissionAndDecreaseNextIntermissionProbability()
        {
            _attenIntermissionCurrentProbability = 0;
            _state = State.inAttenIntermission;
            _sergeant.SayAndAnimate(Main.Instance.Settings.OrdersAndExecutionSettings.AttenIntermissionsAudioClips);
            _sergeant.OnStoppedTalking += EndIntermissionAndIssueNextOrder;
        }

        private void ResetPlayerAction()
        {
            _playerHasAlreadyActed = false;
            _currentPlayerAction = OrdersAndActions.none;
        }

        private void CheckOrderExecutedOnTimedOut()
        {
            if (_isFirstSequence)
                return;

            _soldierAnimation.RequestReturnToNonHutPosition();

            if (_currentOrder.TargetPlayerAction == _currentPlayerAction
                ||
                (
                    _currentOrder.TargetPlayerAction == OrdersAndActions.ten
                    && !_playerHasAlreadyActed
                ))
            {                
                OrderWasExecutedRight();
            }
            else if(_currentPlayerAction != OrdersAndActions.wrong)
            {
                OrderWasExecutedWrong();
            }
        }


        private void OrderWasExecutedRight()
        {
            _score.IncreaseScoreByCurrentIncrement();
        }

        private void OrderWasExecutedWrong()
        {
            _playerHealth.ReduceHealth();
            if (_playerHealth.HasReachedZero)
            {
                InitiateGameOver();
            }
        }

        private void InitiateGameOver()
        {
            _state = State.gameOver;
            _sergeant.OnStoppedTalking -= EndIntermissionAndIssueNextOrder;
            _sergeant.OnStoppedTalking -= RestartTimer;

            OnGameOver.Invoke();
        }

        private void CheckPlayerAction()
        {
            if (_playerInput.CurrentInputAction == OrdersAndActions.none)
                return;

            AnimateSoldier(_playerInput.CurrentInputAction);

            if (_currentPlayerAction == OrdersAndActions.wrong)
                return;

            if (_playerHasAlreadyActed)
            {
                _currentPlayerAction = OrdersAndActions.wrong;
                OrderWasExecutedWrong();
            }
            else
            {
                _currentPlayerAction = _playerInput.CurrentInputAction;
                _playerHasAlreadyActed = true;

                if (_state == State.inAttenIntermission || _currentPlayerAction != _currentOrder.TargetPlayerAction)
                {
                    _currentPlayerAction = OrdersAndActions.wrong;
                    OrderWasExecutedWrong();
                }                
            }
        }

        private void AnimateSoldier(OrdersAndActions currentInputAction)
        {
            _soldierAnimation.RequestAnimation(currentInputAction);
        }

        private void IssueOrder()
        {
            _state = State.orderAudioIsPlaying;
            _currentOrder = _ordersSequence.GetNextOrder();
            _sergeant.SayAndAnimate(_currentOrder);
            _sergeant.OnStoppedTalking += RestartTimer;
        }

        private void IncreaseDifficulty()
        {
            _timerMaxValue /= Main.Instance.Settings.OrdersAndExecutionSettings.DifficultyDelayBetweenOrdersIncrement;

            int randomizedOrdersCountIncrement = Random.Range
                (
                    Main.Instance.Settings.OrdersAndExecutionSettings.DifficultyOrdersInSequenceIncrementMin,
                    Main.Instance.Settings.OrdersAndExecutionSettings.DifficultyOrdersInSequenceIncrementMax + 1
                );
            _ordersSequence.ChangeOrdersInSequenceCountByAmount(randomizedOrdersCountIncrement);

            _sergeant.IncreaseVoiceSpeedBy(Main.Instance.Settings.OrdersAndExecutionSettings.DifficultyVoiceSpeedIncrement);

            _score.IncreaseScoreIncrement();            
        }

        private void ResetDifficulty()
        {
            _timerMaxValue = Main.Instance.Settings.OrdersAndExecutionSettings.InitialDelayBetweenOrders;
            _ordersSequence.Reset(Main.Instance.Settings.OrdersAndExecutionSettings.InitialOrdersInSequenceCount);
            _sergeant.SetVoiceSpeedTo(Main.Instance.Settings.OrdersAndExecutionSettings.InitialVoiceSpeed);
        }

        private void ResetSoldierPosition()
        {
            _soldierAnimation.RequestResetPosition();
        }

        private void RestartTimer()
        {
            _sergeant.OnStoppedTalking -= RestartTimer;
            _timer = _timerMaxValue;
            _state = State.timerIsTicking;
        }
    }
}
