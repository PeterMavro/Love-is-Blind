using UnityEngine;

[RequireComponent(typeof(RPGCharacterController))]
public abstract class Character : MonoBehaviour
{
    private RPGCharacterController _characterController;
    private CharacterAI _characterAI;

    private void Awake()
    {
        _characterController = GetComponent<RPGCharacterController>();
        _characterAI = GetComponent<CharacterAI>();
    }

    public virtual void Select()
    {
        _characterController.SetActiveInput(true);

        _characterAI.enabled = false;
    }

    public virtual void Deselect()
    {
        _characterController.SetActiveInput(false);

        _characterAI.enabled = true;
    }
}