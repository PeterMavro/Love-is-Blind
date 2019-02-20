using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(Animal))]
public class AnimalAI : CharacterAI
{
    [Range(0, 360)]
    public int visionAngle;
    public LayerMask targetLayer;
    [Header("Blocking")]
    public LayerMask blockingLayer;
    public float maxDistance;
    [Header("Patrol")]
    public WayPointsPath path;
    [MinMax(0, 10f)]
    public Vector2 minMaxWaitBetweenPatrolPoints;
    public PatrolFollowLoop followLoop;

    Animal _animal;
    List<Transform> _targetCandidates = new List<Transform>();
    int _currentPatrolPoint = 0;
    float _waitTimer;
    bool _reversePatrol = false, _patrolFinished;
    IHealthHandler _targetHealth;

    private float WaitTimeBetweenPatrolPoints => Random.Range(minMaxWaitBetweenPatrolPoints.x, minMaxWaitBetweenPatrolPoints.y);

    protected override void Awake()
    {
        base.Awake();

        _animal = GetComponent<Animal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer.value) == 0) return;

        _targetCandidates.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer.value) == 0) return;

        _targetCandidates.Remove(other.transform);

        if (m_target == other.transform)
            m_target = null;
    }

    public override void OnUpdate(float deltaTime)
    {
        if (m_target)
        {
            FollowTarget();

            CheckAttack();
        }
        else
        {
            Patrol(deltaTime);

            if (!CheckBlocking())
                SearchTarget();
        }
    }

    public void ResetPatrol()
    {
        _patrolFinished = false;
        _currentPatrolPoint = 0;
    }

    public bool CheckBlocking()
    {
        Ray ray = new Ray(_animal.CatchedTransform.position, _animal.CatchedTransform.forward);

        if (Physics.Raycast(ray, maxDistance, blockingLayer.value))
            return true;

        return false;
    }

    private void CheckAttack()
    {
        if (_targetHealth == null) return;

        if (!_animal.BiteComponent.CanBite) return;

        if (Vector3.Distance(m_target.position, _animal.CatchedTransform.position) < _animal.BiteComponent.biteRange)
        {
            _targetHealth.Damage(_animal.BiteComponent.damage);
            _animal.BiteComponent.ResetTimer();
        }
    }

    private void FollowTarget()
    {
        m_agent.SetDestination(m_target.position);
    }

    private void Patrol(float deltaTime)
    {
        if (path.wayPoints.Length <= 1) return;

        if (_waitTimer == 0)
        {
            m_agent.SetDestination(path.wayPoints[_currentPatrolPoint].position);

            CheckPatrolPointDistance();
        }
        else
        {
            _waitTimer -= deltaTime;

            if (_waitTimer < 0)
            {
                _waitTimer = 0;

                m_agent.isStopped = false;
            }
        }
    }

    private void CheckPatrolPointDistance()
    {
        var distance = Vector3.Distance(path.wayPoints[_currentPatrolPoint].position, _animal.CatchedTransform.position);

        if (distance < (m_agent.stoppingDistance > 2 ? m_agent.stoppingDistance - 1f : 1f))
        {
            _waitTimer = WaitTimeBetweenPatrolPoints;

            ProcessPatrolLoop();

            m_agent.ResetPath();
            m_agent.isStopped = true;
        }
    }

    private void ProcessPatrolLoop()
    {
        switch (followLoop)
        {
            case PatrolFollowLoop.None:
                _currentPatrolPoint++;
                if (_currentPatrolPoint >= path.wayPoints.Length)
                    _patrolFinished = true;
                break;

            case PatrolFollowLoop.Loop:
                _currentPatrolPoint++;
                if (_currentPatrolPoint >= path.wayPoints.Length)
                    _currentPatrolPoint = 0;
                break;

            case PatrolFollowLoop.Pingpong:
                _currentPatrolPoint = _reversePatrol ? (_currentPatrolPoint - 1) : (_currentPatrolPoint + 1);
                if (_currentPatrolPoint >= path.wayPoints.Length || _currentPatrolPoint < 0)
                {
                    _reversePatrol = !_reversePatrol;

                    _currentPatrolPoint = _reversePatrol ? path.wayPoints.Length - 2 : 1;
                }
                break;

            case PatrolFollowLoop.Random:
                _currentPatrolPoint = Random.Range(0, path.wayPoints.Length);
                break;

            default:
                break;
        }
    }

    private void SearchTarget()
    {
        for (int i = 0; i < _targetCandidates.Count; i++)
        {
            Vector3 dir = _targetCandidates[i].position - _animal.CatchedTransform.position;
            if (Vector3.Angle(dir, _animal.CatchedTransform.forward) <= visionAngle)
            {
                m_target = _targetCandidates[i];
                
                _targetHealth = m_target.GetComponent<IHealthHandler>();

                m_agent.isStopped = false;

                break;
            }
        }
    }
}

public enum PatrolFollowLoop
{
    None,
    Loop,
    Pingpong,
    Random
}
