using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

public class PlayerController : MonoBehaviour//, IDamageable
{
    [Header("Components")]
    public Animator animator;
    public CharacterController characterController;
    public List<Collider> colliders;

    [Header("Physics")]
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float jumpSpeed = 15f;
    public float gravity = 9.8f;

    [Header("Run Setup")]
    public float runSpeed = 1.5f;
    public KeyCode keyRun = KeyCode.LeftShift;

    [Header("Health")]
    public HealthBase healthBase;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    private float _vSpeed = 0f;

    private bool _isAlive = true;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void Update()
    {
        if (!_isAlive) return;

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


    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

    private void OnKill(HealthBase h)
    {
        if (_isAlive)
        {
            _isAlive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive()
    {
        _isAlive = true;
        animator.SetTrigger("Revive");
        healthBase.ResetLife();
        Respawn();

        Invoke(nameof(TurnOnColliders), .1f);
    }
    #endregion

    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.PlaceToRespawnPlayer();
        }
    }

    private void TurnOnColliders() 
    {
        colliders.ForEach(i => i.enabled = true);
    }
}
