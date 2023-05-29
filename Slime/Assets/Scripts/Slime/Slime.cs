using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private SlimeManager slimeManager;

    [SerializeField] private SlimeSet slime;
    [SerializeField] private Attacker attackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;

    private Attacker attacker;
    private SlimeMovement movement;
    
    void Start()
    {
        movement = GetComponent<SlimeMovement>();
        UpdateAssets();
        CreateAttacker();
    }
    
    void Update()
    {
        SetValues();
        Vector2 currentPosition = transform.position;
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(dirX, dirY).normalized * moveSpeed;
        movement.MoveTo(currentPosition, move);
    }

    // 슬라임 외형 변경
    void UpdateAssets()
    {
        var resource = "SlimeAnimation/SlimeAnimationOverrideController/" + slime;
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(resource);
    }

    // Attacker 생성
    void CreateAttacker()
    {
        attacker = Instantiate(attackerPrefab);
        attacker.SetSlime(gameObject);
    }

    // gameManager의 값 변경시 적용
    void SetValues()
    {
        if (!ReferenceEquals(slimeManager, null))
        {
            slime = slimeManager.slime;
            attackerPrefab = slimeManager.attackerPrefab;
            attackPrefab = slimeManager.attackPrefab;
            moveSpeed = slimeManager.moveSpeed;
            health = slimeManager.health;
            attackSpeed = slimeManager.attackSpeed;
            damage = slimeManager.damage;
            speed = slimeManager.speed;
            range = slimeManager.range;
        }
    }
    
    // gameManager 연결
    public void SetSlimeManager(GameObject slimeManager)
    {
        this.slimeManager = slimeManager.GetComponent<SlimeManager>();
        transform.SetParent(slimeManager.transform);
    }
}