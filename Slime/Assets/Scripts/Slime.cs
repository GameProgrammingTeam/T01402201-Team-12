using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private SlimeSet slime;
    [SerializeField] private Attacker attackerPrefab;
    [SerializeField] private Attack attackPrefab;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float health;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;

    private Attacker attacker;
    private SlimeMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<SlimeMovement>();
        var resource = "SlimeAnimation/SlimeAnimationOverrideController/" + slime;
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(resource);
        CreateAttacker();
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

    void CreateAttacker()
    {
        attacker = Instantiate(attackerPrefab);
        attacker.SetField(gameObject, attackPrefab);
    }
}