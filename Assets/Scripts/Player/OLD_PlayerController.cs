using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

public class OLD_PlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        IDLE,
        WALKING,
        JUMPING,
    }

    public StateMachine<PlayerStates> stateMachine;

    public Rigidbody myRigidbody;

    [Header("Physics")]
    public float jumpForce = 2f;
    public float speedX = 0f;
    public float speedZ = 0f;
    public float speedMod = 2f;

    [Header("Jump Collision Check")]
    public Collider collider3d;
    public float distToGround;
    public float spaceToGround = .1f;

    // Start is called before the first frame update
    void Awake()
    {
        if (collider3d != null)
            distToGround = collider3d.bounds.extents.y;

        stateMachine = new StateMachine<PlayerStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.IDLE, new StateBase());
        stateMachine.RegisterStates(PlayerStates.WALKING, new StateBase());
        stateMachine.RegisterStates(PlayerStates.JUMPING, new StateBase());

        stateMachine.SwitchState(PlayerStates.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoviment();
        HandleJump();

        if (speedX == 0 && speedZ == 0 && IsGrounded())
            stateMachine.SwitchState(PlayerStates.IDLE);
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.green, distToGround + spaceToGround);

        return Physics.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

    void HandleMoviment()
    {
        if (!IsGrounded()) return;

        if (Input.GetKey(KeyCode.LeftArrow))
            speedX = speedMod * 1;
        else if (Input.GetKey(KeyCode.RightArrow))
            speedX = speedMod * -1;
        else
            speedX = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
            speedZ = speedMod * -1;
        else if (Input.GetKey(KeyCode.DownArrow))
            speedZ = speedMod * 1;
        else
            speedZ = 0f;

        if (speedX != 0 || speedZ != 0)
            stateMachine.SwitchState(PlayerStates.WALKING);

        myRigidbody.velocity = new Vector3(speedX, myRigidbody.velocity.y, speedZ);
    }

    void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            myRigidbody.velocity = Vector3.up * jumpForce;
            stateMachine.SwitchState(PlayerStates.JUMPING);
        }
    }
}
