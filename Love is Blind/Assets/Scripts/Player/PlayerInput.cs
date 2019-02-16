using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    public KeyCode switchCharacterKey;

    private Player _target;

    private void Awake()
    {
        _target = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchCharacterKey))
        {
            _target.SwitchCharacter();
        }
    }
}