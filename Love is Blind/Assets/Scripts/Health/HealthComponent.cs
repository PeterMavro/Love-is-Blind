using System;
using UnityEngine;

public class HealthComponent
{
    public event EventHandler OnHealthChanged;

    private int _maxHealth;
    private float _health;
    private bool _lastWasDamage;

    public int MaxHealth => _maxHealth;
    public bool LastWasDamage => _lastWasDamage;
    public float Health => _health;
    public float HealthPercentage => _health / _maxHealth;

    public HealthComponent(int maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }

    public void Heal(float amount)
    {
        _lastWasDamage = false;

        _health += amount;

        if (_health > _maxHealth) _health = _maxHealth;

        //Debug.Log($"Health: {_health}");

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int amount)
    {
        Heal((float)amount);
    }

    public void Damage(int amount)
    {
        _lastWasDamage = true;

        _health -= amount;

        if (_health < 0) _health = 0;

        //Debug.Log($"Health: {_health}");

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
