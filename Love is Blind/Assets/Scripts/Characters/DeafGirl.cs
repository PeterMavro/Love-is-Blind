using UnityEngine;

public class DeafGirl : Character
{
    private void Start()
    {
        DebugUI.CreateButton(new Vector2(100, 0), "Heal", () => { Heal(10); });

        DebugUI.CreateButton(new Vector2(-100, 0), "Damage", () => { Damage(10); });
    }

    public override void Select()
    {
        base.Select();
    }

    public override void Deselect()
    {
        base.Deselect();
    }
}