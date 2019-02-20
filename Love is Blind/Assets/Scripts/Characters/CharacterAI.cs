using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAI : MonoBehaviour
{
    [SerializeField]
    protected Transform m_target;
    protected NavMeshAgent m_agent;

    protected float m_initSpeed;

    public Transform Target { get => m_target; set { m_target = value; } }

    protected virtual void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();

        m_initSpeed = m_agent.speed;
    }

    public virtual void OnUpdate(float deltaTime)
    {
        if (m_target && m_agent.enabled)
        {
            m_agent.SetDestination(m_target.position);
        }
    }

    public void SetActive(bool active)
    {
        if (m_agent.enabled)
        {
            m_agent.ResetPath();
            m_agent.isStopped = !active;
        }

        m_agent.enabled = active;

        enabled = active;
    }

    public void SetActiveRunBoost(bool active, float multiplier = 1f)
    {
        m_agent.speed = active ? m_agent.speed * multiplier : m_initSpeed;
    }
}