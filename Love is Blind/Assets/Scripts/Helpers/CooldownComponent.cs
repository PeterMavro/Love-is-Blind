using UnityEngine;

public class CooldownComponent : MonoBehaviour
{
    [Range(0, 10f)]
    public float initCooldown;
    public UnityEvents.FloatUnityEvent OnCooldownUpdated;

    protected float m_cooldown;

    public float Cooldown => m_cooldown;
    public float CooldownPercentage => m_cooldown / initCooldown;

    private void Start()
    {
        m_cooldown = 0;

        OnCooldownUpdated?.Invoke(CooldownPercentage);
    }

    public virtual void OnUpdate(float deltaTime)
    {
        if (m_cooldown == 0) return;

        m_cooldown -= deltaTime;

        if (m_cooldown < 0) m_cooldown = 0;

        OnCooldownUpdated?.Invoke(CooldownPercentage);
    }

    public virtual void Reset()
    {
        m_cooldown = initCooldown;
    }
}
