using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworld : MonoBehaviour
{
    private const float MOVE_TIME = 0.175f;
    private const float INVERSE_MOVE_TIME = 1f / MOVE_TIME;
    private const float MOVE_PAUSE_TIME = 0.0125f;

    private bool _isMoving = false;

    private Rigidbody2D _rb2D;

    private void Start()
    {
        this._rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        int directionX = (int) Input.GetAxisRaw("Horizontal");
        int directionY = (int) Input.GetAxisRaw("Vertical");

        if (directionX != 0 || directionY != 0)
        {
            if (directionX != 0) directionY = 0;

            Vector2 endPosition = this._rb2D.position + new Vector2(directionX, directionY);
            AttemptMove(endPosition);
        }
    }

    private void AttemptMove(Vector2 endPosition)
    {
        if (this._isMoving) return;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPosition, LayerMask.GetMask("BlockingLayer"));

        if (hit.collider == null) 
        {
            this._isMoving = true;
            StartCoroutine(SmoothMovement(endPosition));
        }
    }

    private IEnumerator SmoothMovement (Vector2 endPosition)
    {
        float sqrMagnitude = ((Vector2) transform.position - endPosition).sqrMagnitude;
        while (sqrMagnitude > float.Epsilon)
        {
            Vector2 newPosition = Vector2.MoveTowards(this._rb2D.position, endPosition, INVERSE_MOVE_TIME * Time.deltaTime);
            transform.position = newPosition;
            sqrMagnitude = (this._rb2D.position - endPosition).sqrMagnitude;
            yield return null;
        }

        yield return new WaitForSeconds(MOVE_PAUSE_TIME);
        this._isMoving = false;
    }
}
