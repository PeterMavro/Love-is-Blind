using UnityEngine;
using System.Collections;

public class TreeManager : MonoBehaviour
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
}
