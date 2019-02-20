using System;
using UnityEngine;

public class HealthRegenerationComponent : MonoBehaviour
{
    [Range(0f, 20f)]
    public float waitTimeToStartRegenerate;
    [Range(1f, 30f)]
    public float completeRegenerationTime;

    private HealthComponent _healthComponent;
    private float _waitTimer;

    public HealthComponent HealthComponent
    {
        get => _healthComponent;
        set {
            if (value != null)
                value.OnHealthChanged += OnHealthChanged;
            else
            {
                if (_healthComponent != null)
                    _healthComponent.OnHealthChanged -= OnHealthChanged;
            }

            _healthComponent = value;
        }
    }

    public void OnUpdate(float deltaTime)
    {
        if (_healthComponent == null) return;

        if (_waitTimer > 0)
        {
            _waitTimer -= deltaTime;

            if (_waitTimer < 0)
                _waitTimer = 0;
        }
        else if (_waitTimer == 0)
        {
            if (_healthComponent.HealthPercentage < 1)
                _healthComponent.Heal(_healthComponent.MaxHealth * deltaTime / completeRegenerationTime);
            else
                _waitTimer = -1;
        }
    }

    private void OnHealthChanged(object sender, EventArgs args)
    {
        if (_healthComponent.LastWasDamage)
        {
            _waitTimer = waitTimeToStartRegenerate;
        }
    }
}