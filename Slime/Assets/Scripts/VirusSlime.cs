using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSlime : MonoBehaviour
{
    [SerializeField] private SlimeSet slime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float range;
    private GameObject player;
    private SlimeMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Slime");
        movement = GetComponent<SlimeMovement>();
        var resource = "SlimeAnimation/SlimeAnimationOverrideController/" + slime;
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(resource);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 playerPosition = player.transform.position;
        Vector2 move = (playerPosition - currentPosition).normalized * moveSpeed;
        movement.MoveTo(currentPosition, move);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            float damage = other.gameObject.GetComponent<Attack>().damage;
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            Destroy(other.gameObject);
        }
    }

    public void SetField(SlimeSet slime, float moveSpeed, float health, float damage, float attackSpeed, float range)
    {
        this.slime = slime;
        this.moveSpeed = moveSpeed;
        this.health = health;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.range = range;
    }
}