using System.Collections.Generic;

public class BlindBoy : Character
{
    public FogOfWarSystem fogOfWarSystem;

    private List<SpookyTree> _fallingTrees = new List<SpookyTree>();

    private void OnEnable()
    {
        SpookyTree.OnTreeIsGoingToFall += OnTreeIsGoingToFall;
        SpookyTree.OnTreeCompleteFell += OnTreeCompleteFell;
    }

    private void OnDisable()
    {
        SpookyTree.OnTreeIsGoingToFall -= OnTreeIsGoingToFall;
        SpookyTree.OnTreeCompleteFell -= OnTreeCompleteFell;
    }

    public override void Select()
    {
        base.Select();

        fogOfWarSystem.SetActive(true);

        AnimalManager.Instance.ShowAnimals(true);

        PitfallManager.Instance.ShowPitfalls(false);

        //TreeManager.Instance.ChangeTreesToWireframeMaterial(true);
        ChangeTreesToWireframeMaterial(true);
    }

    public override void Deselect()
    {
        base.Deselect();

        fogOfWarSystem.SetActive(false);

        AnimalManager.Instance.ShowAnimals(false);

        PitfallManager.Instance.ShowPitfalls(true);

        //TreeManager.Instance.ChangeTreesToWireframeMaterial(false);
        ChangeTreesToWireframeMaterial(false);
    }

    private void ChangeTreesToWireframeMaterial(bool wireframe)
    {
        for (int i = 0; i < _fallingTrees.Count; i++)
        {
            _fallingTrees[i].ChangeMaterial(wireframe);
        }
    }

    private void OnTreeIsGoingToFall(SpookyTree tree)
    {
        _fallingTrees.Add(tree);

        if (m_selected)
            tree.ChangeMaterial(true);
    }

    private void OnTreeCompleteFell(SpookyTree tree)
    {
        _fallingTrees.Remove(tree);

        if (m_selected)
            tree.ChangeMaterial(false);
    }
}