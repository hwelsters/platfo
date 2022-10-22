using System;
using System.Collections;
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

    private const float GLOW_SPEED = 5f;

    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider2D;
    private Rigidbody2D _rb2D;
    private Material _material;

    private PlayerState _playerState;
    private Coroutine glowCoroutine;
    private bool _stateWasChanged = true;
    private bool _isWinning = false;

    private void OnEnable() { GameManager.OnWin += OnWin; }
    private void OnDisable() { GameManager.OnWin -= OnWin; }

    public void Start()
    {
        // Get needed components
        this._animator = GetComponent<Animator>();
        this._capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        this._rb2D = GetComponent<Rigidbody2D>();
        this._material = GetComponent<SpriteRenderer>().material;

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
        
        if (this._isWinning) 
        {
            this._rb2D.velocity = new Vector2(0, this._rb2D.velocity.y);
            return;
        }
        
        this._playerState.HandleInput(this);
        this._playerState.HandleAnimation(this);
    }

    public void OnWin()
    {
        this._isWinning = true;
        this._playerState.OnWin(this);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void ResetVelocity()
    {
        this._rb2D.velocity = new Vector2(0, 0);
    }

    public void Glow()
    {
        if (glowCoroutine != null) StopCoroutine(glowCoroutine);
        glowCoroutine = StartCoroutine(GlowCoroutine());
    }

    private IEnumerator GlowCoroutine()
    {
        float currentRadian = 0f;
        while (currentRadian < Mathf.PI)
        {
            float brightness = Mathf.Sin(currentRadian);
            currentRadian += Time.deltaTime * GLOW_SPEED;
            this._material.SetFloat("_GlowTime", brightness);
            yield return null;
        }
        this._material.SetFloat("_GlowTime", 0f);
    }
}
