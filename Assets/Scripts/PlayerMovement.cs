using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    public int lives = 3; // Example starting lives

    private bool isDead = false; // Track if the player is dead

    void Update()
    {
        // Only allow movement and input if not dead
        if (!isDead)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, moveY, 0f).normalized;
            transform.Translate(move * speed * Time.deltaTime);

            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);
        }

        // Example death condition: (Replace with condition)
        /*if ( PacStudent should die condition )
        {
            HandleDeath();
        }*/
    }

    private void HandleDeath()
    {
        if (!isDead) // Prevent multiple death handling
        {
            isDead = true; // Mark player as dead
            lives--; // Decrease lives
            animator.SetBool("IsDead", true); // Trigger death animation

            if (lives <= 0)
            {
                // Handle game over (e.g., load game over screen)
            }
            else
            {
                // Optionally: Wait for a few seconds for death animation to play
                // Then: Respawn or restart level
                StartCoroutine(RespawnAfterDelay(3f)); // Example 3-second delay
            }
        }
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Respawn logic here (move player, reset level, etc.)

        // Reset death state
        isDead = false;
        animator.SetBool("IsDead", false);
    }
}
