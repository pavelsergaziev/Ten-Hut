using System;

using OrdersAndExecution;

public class SoldierAnimationController
{
    public event Action OnReturnToInitialPositionRequested = delegate { };
    public event Action OnReturnToNonHutPositionRequested = delegate { };
    public event Action<OrdersAndActions> OnPlayerActionExecuted = delegate { };

    public void RequestAnimation(OrdersAndActions playerAction)
    {
        OnPlayerActionExecuted.Invoke(playerAction);
    }

    public void RequestReturnToNonHutPosition()
    {
        OnReturnToNonHutPositionRequested.Invoke();
    }

    public void RequestResetPosition()
    {
        OnReturnToInitialPositionRequested.Invoke();
    }
}
