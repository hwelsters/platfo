using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggship : Block
{
    private const float DUST_DURATION = 1f;
    private float MOVE_SPEED { get { return Linecast()? 0 : 0.5f; }}

    [SerializeField] private Vector2 direction;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private GameObject eggshipDust;

    private Rigidbody2D rb2D;

    // VERY TEMPORARY FIX
    private Rigidbody2D otherRb2D;
    private Vector2 lastPosition;

    private Vector2 currVelocity { get { return ((Vector2) transform.position - this.lastPosition) / Time.deltaTime; }} 

    private void Start()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        InvokeRepeating("CreateDust", DUST_DURATION, DUST_DURATION);
    }

    protected override void PlayerEnterAction(PlayerMovement playerMovement)
    {
        this.otherRb2D = playerMovement.GetComponent<Rigidbody2D>();
        this.direction *= -1f;
        
        if (this.direction.x < float.Epsilon && this.direction.x > -float.Epsilon) transform.localScale = new Vector2(1f, 1f);
        else transform.localScale = new Vector2(this.direction.x, 1f);
    }

    protected override void PlayerExitAction(PlayerMovement playerMovement)
    {
        this.otherRb2D = null;
    }

    private void FixedUpdate()
    {
        if (this.rb2D != null) this.rb2D.velocity = this.direction * MOVE_SPEED;

        if (this.otherRb2D != null) this.otherRb2D.velocity = this.currVelocity + this.otherRb2D.velocity;
        this.lastPosition = transform.position;
    }

    private bool Linecast()
    {
        Vector2 start = transform.position;

        ChangeLayerMask("Default");
        RaycastHit2D hit = Physics2D.Linecast(start, start + new Vector2(direction.x / 2, direction.y / 2), LayerMask.GetMask("GroundLayer"));
        ChangeLayerMask("GroundLayer");
        
        this.boxCollider2D.enabled = true;

        if (hit.collider != null) return true;
        return false;
    }

    private void ChangeLayerMask(string layerName)
    {
        int layerInt = LayerMask.NameToLayer(layerName);
        gameObject.layer = layerInt;
    }

    private void CreateDust()
    {
        Instantiate(eggshipDust, transform.position, Quaternion.identity);
    }
}
