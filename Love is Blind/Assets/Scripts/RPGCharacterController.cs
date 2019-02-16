using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RPGCharacterController : MonoBehaviour
{
    public float speed = 5;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 dir)
    {
        _controller.SimpleMove(speed * dir);
    }
}
