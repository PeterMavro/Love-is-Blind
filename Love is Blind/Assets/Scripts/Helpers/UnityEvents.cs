using UnityEngine.Events;

public class UnityEvents
{
    [System.Serializable]
    public class FloatUnityEvent : UnityEvent<float>
    { }

    [System.Serializable]
    public class Floatx2UnityEvent : UnityEvent<float, float>
    { }
}