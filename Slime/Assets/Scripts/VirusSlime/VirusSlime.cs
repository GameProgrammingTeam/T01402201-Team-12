using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSlime : MonoBehaviour
{
    [SerializeField] private SlimeSet slime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] public float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float destroyRange;

    private Camera _camera;
    private GameObject player;
    private SlimeMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        player = GameObject.FindWithTag("Slime");
        movement = GetComponent<SlimeMovement>();
        var resource = "SlimeAnimation/SlimeAnimationOverrideController/" + slime;
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(resource);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 playerPosition = player.transform.position;
        Vector2 move = (playerPosition - currentPosition).normalized * moveSpeed;
        movement.MoveTo(currentPosition, move);
        if (Vector2.Distance(_camera.transform.position, transform.position) >= destroyRange)
        {
            Destroy(gameObject);
        }
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

    public void SetField(SlimeSet slime,
        float moveSpeed,
        float health,
        float maxHealth,
        float damage,
        float attackSpeed,
        float destroyRange)
    {
        this.slime = slime;
        this.moveSpeed = moveSpeed;
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.destroyRange = destroyRange;
    }
}