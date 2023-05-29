using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private float _currentTime;
    private float _period = 0.25f;


    void Start()
    { 
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // 슬라임 움직임
    public void MoveTo(Vector2 currentPosition, Vector2 move)
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _period)
        {
            _currentTime -= _currentTime;
        }

        if (move.x != 0 | move.y != 0)
        {
            _animator.speed = 1.0f;
            if (move.x != 0)
            {
                _renderer.flipX = move.x < 0;
            }

            _animator.SetBool("isMoving", true);
            Vector2 movePosition = currentPosition + move * Time.deltaTime;
            transform.position = Vector2.Lerp(currentPosition, movePosition, animationCurve.Evaluate(_currentTime));
        }
        else
        {
            _animator.speed = 0.5f;
            _animator.SetBool("isMoving", false);
        }
    }
}