using UnityEngine;

public class BiteComponent : MonoBehaviour
{
    [Range(0, 100)]
    public int damage;
    public float biteRange;
    [Tooltip("Time between bites")]
    public float biteRate;

    private float _biteRateTimer;

    public bool CanBite => _biteRateTimer == 0;

    public void OnUpdate(float deltaTime)
    {
        if (_biteRateTimer > 0)
        {
            _biteRateTimer -= deltaTime;
            if (_biteRateTimer < 0)
                _biteRateTimer = 0;
        }
    }

    public void ResetTimer()
    {
        _biteRateTimer = biteRate;
    }
}