using UnityEngine;

[RequireComponent(typeof(RPGCharacterController))]
public abstract class Character : MonoBehaviour
{
    private RPGCharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<RPGCharacterController>();
    }

    public virtual void Select()
    {
        _characterController.SetActiveInput(true);
    }

    public virtual void Deselect()
    {
        _characterController.SetActiveInput(false);
    }
}