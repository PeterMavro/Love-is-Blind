using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private NavMeshAgent _agent;
    private CharacterController _charController;
    private MoverType _mover;
    private Animator _anim;
    private bool _runboostActived;
    private int _speedParamHash;

    public MoverType Mover {
        get => _mover;
        set => _mover = value;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _charController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _speedParamHash = Animator.StringToHash("Speed");

        OnBoostFinished();
    }

    public void OnUpdate(float deltaTime)
    {
        switch (_mover)
        {
            case MoverType.NavMeshAgent:
                if (_agent.velocity.sqrMagnitude > 0)
                    _anim.SetFloat(_speedParamHash, _runboostActived ? 1f : 0.5f, 0.1f, deltaTime);
                else
                    _anim.SetFloat(_speedParamHash, 0f, 0.1f, deltaTime);
                break;
            case MoverType.CharacterController:
                if (_charController.velocity.sqrMagnitude > 0)
                    _anim.SetFloat(_speedParamHash, _runboostActived ? 1f : 0.5f, 0.1f, deltaTime);
                else
                    _anim.SetFloat(_speedParamHash, 0f, 0.1f, deltaTime);
                break;
            default:
                break;
        }
    }

    public void OnHealthChanged(float percentage)
    {
        if (percentage > 0)
            _anim.SetTrigger("GetHit");
        else
            _anim.SetTrigger("Faint");
    }

    public void OnBoostStarted()
    {
        _runboostActived = true;
    }

    public void OnBoostFinished()
    {
        _runboostActived = false;
    }
}

public enum MoverType
{
    NavMeshAgent,
    CharacterController
}
