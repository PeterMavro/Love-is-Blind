using UnityEngine;
using System.Collections.Generic;

public class PitfallManager : Singleton<PitfallManager>
{
    [SerializeField]
    private List<Pitfall> _pitfalls = new List<Pitfall>();

    public void Add(Pitfall entity)
    {
        if (!_pitfalls.Contains(entity))
            _pitfalls.Add(entity);
    }

    public void Remove(Pitfall entity)
    {
       _pitfalls.Remove(entity);
    }

    public void ShowPitfalls(bool show)
    {
        for (int i = 0; i < _pitfalls.Count; i++)
        {
            _pitfalls[i].RendererComponent.SetActive(show);
        }
    }
}