using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform _target;

    public Transform Target { get => _target; set { _target = value; } }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_target)
        {
            _agent.SetDestination(_target.position);
        }
    }
}