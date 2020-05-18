using System;

using OrdersAndExecution;

public class SergeantAnimationController
{
    public event Action<OrderSO> OnOrderAnimationRequested = delegate { };
    public event Action OnNonOrderAnimationRequested = delegate { };

    public void LaunchOrderAnimation(OrderSO order)
    {
        OnOrderAnimationRequested.Invoke(order);
    }

    public void LaunchNonOrderAnimation()
    {
        OnNonOrderAnimationRequested.Invoke();
    }
}
