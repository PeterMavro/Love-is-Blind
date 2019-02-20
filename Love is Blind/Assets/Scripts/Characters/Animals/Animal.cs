using UnityEngine;

public class Animal : Character
{
    private BiteComponent _biteComponent;

    public BiteComponent BiteComponent => _biteComponent;

    protected override void Awake()
    {
        base.Awake();

        _biteComponent = GetComponent<BiteComponent>();
    }

    private void Start()
    {
        Deselect();
    }

    public override void OnUpdated(float deltaTime)
    {
        base.OnUpdated(deltaTime);

        _biteComponent.OnUpdate(deltaTime);
    }
}
