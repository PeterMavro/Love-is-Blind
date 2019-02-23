using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(RPGCharacterControllerInput))]
// AI
public class RPGCharacterController : MonoBehaviour
{
    public float initSpeed = 5;

    private float _speed;

    private CharacterController _controller;
    private RPGCharacterControllerInput _input;
    private Transform _catchedTransform;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<RPGCharacterControllerInput>();

        _catchedTransform = transform;
    }

    private void Start()
    {
        _speed = initSpeed;
    }

    public void Move(Vector3 dir)
    {
        if (dir != Vector3.zero)
            _catchedTransform.rotation = Quaternion.Slerp(_catchedTransform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);

        _controller.SimpleMove(_speed * dir);
    }

    public void SetInputActive(bool active)
    {
        _input.enabled = active;
    }

    public void SetActiveRunBoost(bool active, float multiplier = 1f)
    {
        _speed = active ? _speed * multiplier : initSpeed;
    }
}
