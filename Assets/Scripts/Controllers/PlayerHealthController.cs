using System;

public class PlayerHealthController
{
    private int _currentHealth;
    private int _maxHealth;
    public int MaxHealth { get { return _maxHealth; } }
    public bool HasReachedZero { get { return _currentHealth <= 0; } }

    public event Action<int> OnCurrentHealthChanged = delegate { };

    public PlayerHealthController()
    {
        _maxHealth = Main.Instance.Settings.OrdersAndExecutionSettings.AmountOfWrongMovesToEndGame;
        ResetHealth();
    }

    public void ReduceHealth()
    {
        OnCurrentHealthChanged.Invoke(--_currentHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        OnCurrentHealthChanged.Invoke(_currentHealth);
    }
}

