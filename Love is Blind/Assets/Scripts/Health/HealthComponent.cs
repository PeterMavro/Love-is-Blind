using System;
using UnityEngine;

public class HealthComponent
{
    public event EventHandler OnHealthChanged;

    private int _maxHealth;
    private int _health;

    public int Health => _health;
    public float HealthPercentage => (float)_health / _maxHealth;

    public HealthComponent(int maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }

    public void Heal(int amount)
    {
        _health += amount;

        if (_health > _maxHealth) _health = _maxHealth;

        Debug.Log($"Health: {_health}");

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Damage(int amount)
    {
        _health -= amount;

        if (_health < 0) _health = 0;

        Debug.Log($"Health: {_health}");

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
