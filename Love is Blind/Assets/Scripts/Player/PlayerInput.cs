using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    public string switchCharacterButton = "Switch";
    public string characterRunboostButton = "RunBoost";

    private Player _target;

    private void Awake()
    {
        _target = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(switchCharacterButton))
        {
            _target.SwitchCharacter();
        }

        if (Input.GetButtonDown(characterRunboostButton))
        {
            _target.ActiveRunBoost();
        }
    }
}