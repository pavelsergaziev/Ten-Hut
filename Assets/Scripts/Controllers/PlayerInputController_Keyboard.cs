using System;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerInputController_Keyboard : IPlayerInput, IUpdated, IDependencyInjectionReceiver
    {
        private Updater _updater;

        public event Action<PlayerInputEnum> InputEvent;

        public PlayerInputController_Keyboard()
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
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                InvokeInputEvent(PlayerInputEnum.enter);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                InvokeInputEvent(PlayerInputEnum.up);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                InvokeInputEvent(PlayerInputEnum.down);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                InvokeInputEvent(PlayerInputEnum.left);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                InvokeInputEvent(PlayerInputEnum.right);
            }
        }

        private void InvokeInputEvent(PlayerInputEnum inputValue)
        {
            InputEvent.Invoke(inputValue);
        }
    }

}
