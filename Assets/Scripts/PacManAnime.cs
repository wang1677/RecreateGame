using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Sprite[] AnimationSprites;
    public float FrameDuration = 0.25f;
    public bool IsLooping = true;

    private int _currentFrame;
    private Coroutine _animationCoroutine;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Start the animation coroutine
        _animationCoroutine = StartCoroutine(AnimateSprites());
    }

    private IEnumerator AnimateSprites()
    {
        while (true)
        {
            if (AnimationSprites.Length == 0)
            {
                yield break;
            }

            // Wait for the frame duration
            yield return new WaitForSeconds(FrameDuration);

            if (!SpriteRenderer.enabled)
            {
                continue;
            }

            // Advance to the next frame
            _currentFrame++;
            if (_currentFrame >= AnimationSprites.Length)
            {
                if (IsLooping)
                {
                    _currentFrame = 0;
                }
                else
                {
                    // Stop the coroutine if not looping
                    yield break;
                }
            }

            // Update the sprite
            SpriteRenderer.sprite = AnimationSprites[_currentFrame];
        }
    }

    public void ResetAnimation()
    {
        _currentFrame = -1;

        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimateSprites());
    }

    private void OnDisable()
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }
    }
}