using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerInput;
using System;

namespace OrdersAndExecution
{

    public class PlayerActionsInputController : IUpdated, IDependencyInjectionReceiver
    {
        public OrdersAndActions CurrentInputAction { get; private set; }

        private Updater _updater;
        private IPlayerInput _input;

        private bool _inputResolvedThisFrame;

        public PlayerActionsInputController()
        {
            Main.Instance.SubscribeToDependencyInjection(this);
        }

        public void InjectDependencies()
        {
            _updater = Main.Instance.Updater;
            _updater.Subscribe(this);

            _input = Main.Instance.PlayerInputController;
            _input.InputEvent += ResolveInput;
        }


        private void ResolveInput(PlayerInputEnum input)
        {
            _inputResolvedThisFrame = true;

            switch (input)
            {
                case PlayerInputEnum.up:
                    CurrentInputAction = OrdersAndActions.hut;
                    break;
                case PlayerInputEnum.left:
                    CurrentInputAction = OrdersAndActions.left;
                    break;
                case PlayerInputEnum.right:
                    CurrentInputAction = OrdersAndActions.right;
                    break;
                default:
                    break;
            }
        }

        public void Tick()
        {
            if (_inputResolvedThisFrame)
            {
                _inputResolvedThisFrame = false;
            }
            else
            {
                CurrentInputAction = OrdersAndActions.none;
            }            
        }
    }

}