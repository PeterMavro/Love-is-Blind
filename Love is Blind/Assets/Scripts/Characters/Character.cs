using System;
using UnityEngine;

[RequireComponent(typeof(RPGCharacterController))]
public abstract class Character : MonoBehaviour, IHealthHandler
{
    [Range(0, 100)]
    public int maxHealth = 100;
    [Space]
    public UnityEvents.FloatUnityEvent OnHealthChanged;

    private RPGCharacterController _characterController;
    private CharacterAI _characterAI;

    protected HealthComponent m_healthComponent;

    private void Awake()
    {
        _characterController = GetComponent<RPGCharacterController>();
        _characterAI = GetComponent<CharacterAI>();

        m_healthComponent = new HealthComponent(maxHealth);
    }

    private void OnEnable()
    {
        m_healthComponent.OnHealthChanged += OnHealthChangedCallback;
    }

    private void OnDisable()
    {
        m_healthComponent.OnHealthChanged -= OnHealthChangedCallback;
    }

    private void OnHealthChangedCallback(object sender, EventArgs args)
    {
        OnHealthChanged?.Invoke(m_healthComponent.HealthPercentage);
    }

    public virtual void Select()
    {
        _characterController.SetInputActive(true);

        _characterAI.SetActive(false);
    }

    public virtual void Deselect()
    {
        _characterController.SetInputActive(false);

        _characterAI.SetActive(true);
    }

    public virtual void ActiveRunBoost(float multiplier)
    {
        _characterController.SetActiveRunBoost(true, multiplier);
        _characterAI.SetActiveRunBoost(true, multiplier);
    }

    public virtual void DesactiveRunBoost()
    {
        _characterController.SetActiveRunBoost(false);
        _characterAI.SetActiveRunBoost(false);
    }

    #region IHealthHandler methods
    public void Heal(int amount)
    {
        m_healthComponent.Heal(amount);
    }

    public void Damage(int amount)
    {
        m_healthComponent.Damage(amount);
    }
    #endregion
}