using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public Animator animator; // Reference to the Animator component

    void Update()
    {
        // Get input
        float moveX = Input.GetAxis("Horizontal"); // Left/Right
        float moveY = Input.GetAxis("Vertical");   // Up/Down

        // Move the player
        Vector3 move = new Vector3(moveX, moveY, 0f).normalized;
        transform.Translate(move * speed * Time.deltaTime);

        // Update animation parameters
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
    }
}
