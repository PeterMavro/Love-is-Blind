using UnityEngine;
using UnityEngine.Events;

public class CharacterRunBoostComponent : CooldownComponent
{
    public float initDuration;
    [Range(0f, 5f)]
    public float boostMultiplier;
    public UnityEvent OnBoostStarted;
    public UnityEvents.FloatUnityEvent OnDurationUpdated;
    public UnityEvent OnBoostFinished;

    private float _duration;

    public float Duration => _duration;
    public float DurationPercentage => _duration / initDuration;

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if (_duration == 0) return;

        _duration -= deltaTime;

        if (_duration < 0) _duration = 0;

        OnDurationUpdated?.Invoke(DurationPercentage);

        if (_duration == 0)
            OnBoostFinished?.Invoke();
    }

    public override void DoReset()
    {
        base.DoReset();

        _duration = initDuration;

        OnBoostStarted?.Invoke();
    }
}
