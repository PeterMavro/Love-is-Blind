using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAI : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private NavMeshAgent _agent;

    private float _initSpeed;

    public Transform Target { get => _target; set { _target = value; } }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _initSpeed = _agent.speed;
    }

    private void Update()
    {
        if (_target)
        {
            _agent.SetDestination(_target.position);
        }
    }

    public void SetActive(bool active)
    {
        if (!active)
        {
            _agent.isStopped = active;
            _agent.ResetPath();
        }

        enabled = active;
    }

    public void SetActiveRunBoost(bool active, float multiplier = 1f)
    {
        _agent.speed = active ? _agent.speed * multiplier : _initSpeed;
    }
}