using UnityEngine;

public class TreeManager : Singleton<TreeManager>
{
    public SpookyTree[] trees;

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].OnUpdate(deltaTime);
        }
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
