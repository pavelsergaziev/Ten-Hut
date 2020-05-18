using System;

namespace PlayerInput
{
    public interface IPlayerInput
    {
        event Action<PlayerInputEnum> InputEvent;
    }
}