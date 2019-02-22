using System.Collections.Generic;
using UnityEngine;

public class TreeManager : Singleton<TreeManager>
{
    [SerializeField]
    private List<SpookyTree> _trees = new List<SpookyTree>();

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        for (int i = 0; i < _trees.Count; i++)
        {
            _trees[i].OnUpdate(deltaTime);
        }
    }

    public void Add(SpookyTree entity)
    {
        if (!_trees.Contains(entity))
            _trees.Add(entity);
    }

    public void Remove(SpookyTree entity)
    {
        _trees.Remove(entity);
    }

    public void ChangeTreesToWireframeMaterial(bool wireframe)
    {
        for (int i = 0; i < _trees.Count; i++)
        {
            if (_trees[i].fallProbability > 0)
                _trees[i].RendererComponent.ChangeMaterial(wireframe ? 1 : 0);
        }
    }
}
