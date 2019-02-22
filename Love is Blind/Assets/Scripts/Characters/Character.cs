using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RPGCharacterController))]
[RequireComponent(typeof(CharacterAI))]
public abstract class Character : MonoBehaviour, IHealthHandler
{
    [Range(0, 100)]
    public int maxHealth = 100;
    [Space]
    public UnityEvents.FloatUnityEvent OnHealthChanged;

    private RPGCharacterController _characterController;
    private CharacterAI _characterAI;

    protected HealthComponent m_healthComponent;
    protected HealthRegenerationComponent m_healthRegenerationComponent;
    protected Transform m_catchedTransform;
    protected bool m_selected;

    public Transform CatchedTransform => m_catchedTransform;

    protected virtual void Awake()
    {
        _characterController = GetComponent<RPGCharacterController>();
        _characterAI = GetComponent<CharacterAI>();

        m_healthRegenerationComponent = GetComponent<HealthRegenerationComponent>();

        m_healthComponent = new HealthComponent(maxHealth);

        m_catchedTransform = transform;
    }

    protected virtual void Start()
    {
        if (m_healthRegenerationComponent)
            m_healthRegenerationComponent.HealthComponent = m_healthComponent;
    }

    private void OnEnable()
    {
        m_healthComponent.OnHealthChanged += OnHealthChangedCallback;
    }

    private void OnDisable()
    {
        m_healthComponent.OnHealthChanged -= OnHealthChangedCallback;
    }

    public virtual void OnUpdated(float deltaTime)
    {
        _characterAI.OnUpdate(deltaTime);

        if (m_healthRegenerationComponent)
            m_healthRegenerationComponent.OnUpdate(deltaTime);
    }

    private void OnHealthChangedCallback(object sender, EventArgs args)
    {
        OnHealthChanged?.Invoke(m_healthComponent.HealthPercentage);

        if (m_healthComponent.HealthPercentage == 0)
        {
            PlayerManager.Instance.localPlayer.SetCharacterInputActive(false);
            PlayerManager.Instance.localPlayer.SendGameOver(GameResult.Lose);
        }
    }

    public virtual void Select()
    {
        m_selected = true;

        _characterController.SetInputActive(true);

        _characterAI.SetActive(false);
    }

    public virtual void Deselect()
    {
        m_selected = false;

        _characterController.SetInputActive(false);

        _characterAI.SetActive(true);
    }

    /// <summary>
    /// Moving constantly every frame makes CharacterController works as expected
    /// </summary>
    /// <param name="active"></param>
    public void SetInputActive(bool active)
    {
        _characterController.SetInputActive(active);
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