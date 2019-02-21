using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpookyTree : MonoBehaviour
{
    public LayerMask targetLayer;
    public Transform treeTop;
    [Tooltip("The time between each calculus of fall probability")]
    public Vector2 minMaxfallProbabilityRate;
    [Range(0, 1f)]
    public float fallProbability;
    public float fallForce;

    [SerializeField]
    private List<Transform> _targetCandidates = new List<Transform>();
    private float _probabilityRateTimer;
    private Rigidbody _rigidbody;
    private Transform _catchedTransform;
    private bool _fell = false;

    private void Awake()
    {
        _catchedTransform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer.value) != 0)
        {
            _targetCandidates.Add(other.transform);
        }
    }

    public void OnUpdate(float deltaTime)
    {
        if (_fell) return;

        if (_targetCandidates.Count > 0)
        {
            if (_probabilityRateTimer == 0)
                CalculateFallProbability();
            else if (_probabilityRateTimer > 0)
            {
                _probabilityRateTimer -= deltaTime;
                if (_probabilityRateTimer < 0)
                    _probabilityRateTimer = 0;
            }
        }
    }

    public void DoReset()
    {
        _fell = false;
        _rigidbody.isKinematic = true;
    }

    private void CalculateFallProbability()
    {
        var d = Random.value;

        if (d < fallProbability)
        {
            FallOff();
        }

        _probabilityRateTimer = Random.Range(minMaxfallProbabilityRate.x, minMaxfallProbabilityRate.y);
    }

    private void FallOff()
    {
        var rd = Random.Range(0, _targetCandidates.Count);
        var dir = _targetCandidates[rd].position - _catchedTransform.position;

        _rigidbody.isKinematic = false;
        _rigidbody.AddForceAtPosition(dir * fallForce, treeTop.position);

        _fell = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer.value) != 0)
        {
            _targetCandidates.Remove(other.transform);
        }
    }
}
