using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator Animator { get { return this._animator; } }
    public CapsuleCollider2D CapsuleCollider2D { get { return this._capsuleCollider2D; } }
    public Rigidbody2D Rigidbody2D { get { return this._rb2D; } }
    public PlayerState PlayerState
    {
        get { return this._playerState; }
        set
        {
            this._stateWasChanged = true;
            this._playerState = value;
        }
    }

    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider2D;
    private Rigidbody2D _rb2D;

    private PlayerState _playerState;
    private bool _stateWasChanged = true;

    public void Start()
    {
        this._animator = GetComponent<Animator>();
        this._capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        this._rb2D = GetComponent<Rigidbody2D>();

        this._playerState = new PlayerBaseState();

        // Configure Rigidbody2D
        this._rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void Update()
    {
        if (this._stateWasChanged)
        {
            this._playerState.HandleEnter(this);
            this._stateWasChanged = false;
        }

        this._playerState.HandleInput(this);
        this._playerState.HandleAnimation(this);
    }

    public void OnWin()
    {
        this._playerState.OnWin();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void ResetVelocity()
    {
        this._rb2D.velocity = new Vector2(0, 0);
    }
}
