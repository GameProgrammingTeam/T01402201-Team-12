using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;

    private SpriteRenderer renderer;
    private Animator animator;
    private float currentTime;
    private float period = 0.25f;


    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // 슬라임 움직임
    public void MoveTo(Vector2 currentPosition, Vector2 move)
    {
        currentTime += Time.deltaTime;
        if (currentTime >= period)
        {
            currentTime -= currentTime;
        }

        if (move.x != 0 | move.y != 0)
        {
            animator.speed = 1.0f;
            if (move.x != 0)
            {
                renderer.flipX = move.x < 0;
            }

            animator.SetBool("isMoving", true);
            Vector2 movePosition = currentPosition + move * Time.deltaTime;
            transform.position = Vector2.Lerp(currentPosition, movePosition, animationCurve.Evaluate(currentTime));
        }
        else
        {
            animator.speed = 0.5f;
            animator.SetBool("isMoving", false);
        }
    }
}