using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private SlimeSet slime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    private SlimeMovement movement;
    private Attacker attacker;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<SlimeMovement>();
        var resource = "SlimeAnimation/SlimeAnimationOverrideController/" + slime;
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(resource);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(dirX, dirY).normalized * moveSpeed;
        movement.MoveTo(currentPosition, move);
    }
}