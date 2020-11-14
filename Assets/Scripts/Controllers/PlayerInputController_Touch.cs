//Можно ещё попробовать прицепить детект движения девайса, чтобы движением телефона вверх выполнять приказ Hut.
//Но для этого акселерометра не хватает, тут нужен gyro на телефоне
//(и тогда брать инфу о линейном перемещении девайса из Input.gyro.userAcceleration).

//Свайп простейший, без отмен при задержке движения и небольшом движении пальца назад
//, но здесь он вполне подойдет, как мне кажется.

using System;
using UnityEngine;

namespace PlayerInput
{

    public class PlayerInputController_Touch : IPlayerInput, IUpdated, IDependencyInjectionReceiver
    {
        private Updater _updater;

        private Vector2 _touchStartPosition;
        private float _swipeDistanceX, _swipeDistanceY;
        PlayerInputEnum _playerInputValue;
        //вероятно, ещё добавить некое минимальное значение для засчитывания свайпа

        public event Action<PlayerInputEnum> InputEvent;

        public PlayerInputController_Touch()
        {
            Main.Instance.SubscribeToDependencyInjection(this);
        }

        public void InjectDependencies()
        {
            _updater = Main.Instance.Updater;
            _updater.SubscribeAndPrioritize(this);
        }

        public void Tick()
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    SwipeToPosition(touch.position);
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }

        }

        private void SwipeToPosition(Vector2 touchEndPosition)
        {
            _swipeDistanceX = touchEndPosition.x - _touchStartPosition.x;
            _swipeDistanceY = touchEndPosition.y - _touchStartPosition.y;

            if(Mathf.Abs(_swipeDistanceX) > Mathf.Abs(_swipeDistanceY))
            {
                _playerInputValue = _swipeDistanceX > 0 ? PlayerInputEnum.right : PlayerInputEnum.left;
            }
            else
            {
                _playerInputValue = _swipeDistanceY > 0 ? PlayerInputEnum.up : PlayerInputEnum.down;
            }

            InvokeInputEvent(_playerInputValue);
        }

        private void InvokeInputEvent(PlayerInputEnum inputValue)
        {
            InputEvent.Invoke(inputValue);
        }
    }

}