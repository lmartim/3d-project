using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public CharacterController characterController;

    [Header("Physics")]
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float jumpSpeed = 15f;
    public float gravity = 9.8f;

    [Header("Run Setup")]
    public float runSpeed = 1.5f;
    public KeyCode keyRun = KeyCode.LeftShift;

    private float _vSpeed = 0f;

    private void Update()
    {
        var turnFactor = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turnFactor, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector= transform.forward * inputAxisVertical * speed;

        // Updates _vSpeed value
        Jump();

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= runSpeed;
                animator.speed = runSpeed;
            }
            else
            {
                animator.speed = 1f;
            }
        }

        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", isWalking);
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            _vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _vSpeed = jumpSpeed;
            }
        }
    }
}
