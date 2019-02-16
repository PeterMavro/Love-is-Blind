
public class BlindBoy : Character
{
    public FogOfWarSystem fogOfWarSystem;

    public override void Select()
    {
        base.Select();

        fogOfWarSystem.SetActive(true);
    }

    public override void Deselect()
    {
        base.Deselect();

        fogOfWarSystem.SetActive(false);
    }
}