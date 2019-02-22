using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class SpookyTree : MonoBehaviour
{
    public static event Action<SpookyTree> OnTreeIsGoingToFall;
    public static event Action<SpookyTree> OnTreeCompleteFell;

    public RendererComponent RendererComponent => _renderer;

    public LayerMask targetLayer;
    public Transform treeTop;
    [Tooltip("The time between each calculus of fall probability")]
    public Vector2 minMaxfallProbabilityRate;
    [Range(0, 1f)]
    public float fallProbability;
    public float fallForce;
    [Tooltip("The time to wait after fall to reset the tree to the isKinematic true state")]
    public float fallTimeToResetKinematic = 4f;

    private List<Transform> _targetCandidates = new List<Transform>();
    private float _probabilityRateTimer;
    private bool _fell = false;

    private Rigidbody _rigidbody;
    private Transform _catchedTransform;
    private RendererComponent _renderer;

    private void Awake()
    {
        _catchedTransform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<RendererComponent>();
    }

    private void OnEnable()
    {
        TreeManager.Instance.Add(this);
    }

    private void OnDisable()
    {
        TreeManager.Instance.Remove(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (fallProbability == 0) return;

        if (((1 << other.gameObject.layer) & targetLayer.value) != 0)
        {
            _targetCandidates.Add(other.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (fallProbability == 0) return;

        if (((1 << collision.gameObject.layer) & targetLayer.value) != 0)
        {
            PlayerManager.Instance.localPlayer.SetCharacterInputActive(false);
            PlayerManager.Instance.localPlayer.SendGameOver(GameResult.Lose);
        }
    }

    public void OnUpdate(float deltaTime)
    {
        if (_fell || fallProbability == 0) return;

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

        ResetStaticTree();
    }

    private void CalculateFallProbability()
    {
        var d = UnityEngine.Random.value;

        if (d < fallProbability)
        {
            OnTreeIsGoingToFall?.Invoke(this);

            FallOff();
        }

        _probabilityRateTimer = UnityEngine.Random.Range(minMaxfallProbabilityRate.x, minMaxfallProbabilityRate.y);
    }

    private void FallOff()
    {
        var rd = UnityEngine.Random.Range(0, _targetCandidates.Count);
        var dir = _targetCandidates[rd].position - _catchedTransform.position;

        _rigidbody.isKinematic = false;
        _rigidbody.AddForceAtPosition(dir * fallForce, treeTop.position);

        _fell = true;

        Invoke("ResetStaticTree", fallTimeToResetKinematic);
    }

    private void ResetStaticTree()
    {
        _rigidbody.isKinematic = true;

        OnTreeCompleteFell?.Invoke(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (fallProbability == 0) return;

        if (((1 << other.gameObject.layer) & targetLayer.value) != 0)
        {
            _targetCandidates.Remove(other.transform);
        }
    }

    public void ChangeMaterial(bool wireframe)
    {
        _renderer.ChangeMaterial(wireframe ? 1 : 0);
    }
}
