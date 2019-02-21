
public class BlindBoy : Character
{
    public FogOfWarSystem fogOfWarSystem;

    public override void Select()
    {
        base.Select();

        fogOfWarSystem.SetActive(true);

        AnimalManager.Instance.ShowAnimals(true);

        TreeManager.Instance.ChangeTreesToWireframeMaterial(true);
    }

    public override void Deselect()
    {
        base.Deselect();

        fogOfWarSystem.SetActive(false);

        AnimalManager.Instance.ShowAnimals(false);

        TreeManager.Instance.ChangeTreesToWireframeMaterial(false);
    }
}