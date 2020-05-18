using System;

public class UIButtonEffectsController
{
    public event Action<MenuButtons> OnButtonPressed = delegate { };

    public void ButtonEffectRequested(MenuButtons button)
    {
        OnButtonPressed.Invoke(button);
    }
}
