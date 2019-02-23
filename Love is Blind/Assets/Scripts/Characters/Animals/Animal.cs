using UnityEngine;

public class Animal : Character
{
    private BiteComponent _biteComponent;
    private RendererComponent _renderer;

    public BiteComponent BiteComponent => _biteComponent;
    public RendererComponent RendererComponent => _renderer;

    protected override void Awake()
    {
        base.Awake();

        _biteComponent = GetComponent<BiteComponent>();
        _renderer = GetComponent<RendererComponent>();
    }

    protected override void OnEnable()
    {
        AnimalManager.Instance.Add(this);
    }

    protected override void OnDisable()
    {
        AnimalManager.Instance.Remove(this);
    }

    protected override void Start()
    {
        base.Start();

        Deselect();
    }

    public override void OnUpdated(float deltaTime)
    {
        m_characterAI.OnUpdate(deltaTime);

        //m_animator.OnUpdate(deltaTime);

        if (m_healthRegenerationComponent)
            m_healthRegenerationComponent.OnUpdate(deltaTime);

        _biteComponent.OnUpdate(deltaTime);
    }
}
