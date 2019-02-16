using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(RPGCharacterControllerInput))]
// AI
public class RPGCharacterController : MonoBehaviour
{
    public float speed = 5;

    private CharacterController _controller;
    private RPGCharacterControllerInput _input;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<RPGCharacterControllerInput>();
    }

    public void Move(Vector3 dir)
    {
        _controller.SimpleMove(speed * dir);
    }

    public void SetActiveInput(bool active)
    {
        _input.enabled = active;
    }
}
