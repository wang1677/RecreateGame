using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public AudioClip eatSound;
    public AudioClip moveSound;
    public LayerMask wallLayer;
    public Transform pelletParent;

    private AudioSource audioSource;
    private Vector2 direction;
    private Vector2 nextDirection;
    private Vector2 lastInput;
    private Vector2 currentInput;
    private bool isMoving;
    private Vector2 desiredPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        desiredPosition = transform.position;
        ResetPellets();
        direction = Vector2.right; // Default direction if you have one
    }

    void Update()
    {
        if (!isMoving)
        {
            CheckForInput();
            TryMove();
        }

        if ((Vector2)transform.position == desiredPosition)
        {
            isMoving = false;
            CheckForPellet();
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
        }
    }

    private void CheckForInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input != Vector2.zero)
        {
            lastInput = input;
        }
    }

    private void TryMove()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + lastInput;

        if (!WallInDirection(lastInput))
        {
            currentInput = lastInput;
            desiredPosition = endPos;
            isMoving = true;
            PlaySound(moveSound);
        }
        else if (!WallInDirection(currentInput))
        {
            desiredPosition = startPos + currentInput;
            isMoving = true;
            PlaySound(moveSound);
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.fixedDeltaTime);
    }

    private bool WallInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, (Vector2)transform.position + direction, wallLayer);
        return hit.collider != null;
    }

    private void CheckForPellet()
    {
        // Assuming your pellets are in a layer called "Pellets"
        int pelletLayer = LayerMask.NameToLayer("Pellets");
        Collider2D pellet = Physics2D.OverlapPoint(transform.position, 1 << pelletLayer);
        if (pellet)
        {
            pellet.gameObject.SetActive(false); // Eat the pellet
            PlaySound(eatSound); // Play eating sound
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ResetPellets()
    {
        foreach (Transform pellet in pelletParent)
        {
            pellet.gameObject.SetActive(true); // Reactivate all pellets for a new game round
        }
    }
}