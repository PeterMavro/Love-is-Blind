using UnityEngine;

[RequireComponent(typeof(RPGCharacterController))]
public class RPGCharacterControllerInput : MonoBehaviour
{
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    private RPGCharacterController _target;

    private void Awake()
    {
        _target = GetComponent<RPGCharacterController>();
    }

    private void Update()
    {
        var hor = Input.GetAxis(horizontalAxis);
        var ver = Input.GetAxis(verticalAxis);

        _target.Move(new Vector3(hor, 0, ver));
    }
}