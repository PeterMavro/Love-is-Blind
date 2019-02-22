using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance {
        get {
            if (!_instance)
            {
                var all = FindObjectsOfType<T>();
                if (all.Length > 0)
                {
                    _instance = all[0];

                    for (int i = 1; i < all.Length; i++)
                    {
                        DestroyImmediate(all[i].gameObject);
                    }
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != (this as T))
        {
            DestroyImmediate(gameObject);
            return;
        }

        _instance = this as T;
    }
}
