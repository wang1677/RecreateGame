using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    private Vector2 direction;
    private Vector2 nextDirection;
    private Vector3 startingPosition;

    private void Awake()
    {
        startingPosition = transform.position;
        direction = initialDirection;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        enabled = true;
    }

    private void Update()
    {
        if (nextDirection != Vector2.zero && !Occupied(nextDirection))
        {
            direction = nextDirection;
            nextDirection = Vector2.zero;
        }

        Vector3 newPosition = transform.position + (new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime);
        if (!Occupied(direction))
        {
            transform.position = newPosition;
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        if (!Occupied(newDirection))
        {
            direction = newDirection;
        }
        else
        {
            nextDirection = newDirection;
        }
    }

    private bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, obstacleLayer);
        return hit.collider != null;
    }
}