using System.Collections.Generic;
using UnityEngine;

public class TreeManager : Singleton<TreeManager>
{
    public SpookyTree[] trees;

    [SerializeField]
    private List<SpookyTree> _trees = new List<SpookyTree>();

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].OnUpdate(deltaTime);
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
        for (int i = 0; i < trees.Length; i++)
        {
            if (trees[i].fallProbability > 0)
                trees[i].RendererComponent.ChangeMaterial(wireframe ? 1 : 0);
        }
    }
}
