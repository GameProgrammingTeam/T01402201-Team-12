using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] public SlimeSet slime;
    [SerializeField] public Attacker attackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float health;
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
        if (!ReferenceEquals(gameManager, null))
        {
            slime = gameManager.slime;
            attackerPrefab = gameManager.slimeAttackerPrefab;
            attackPrefab = gameManager.slimeAttackPrefab;
            moveSpeed = gameManager.slimeMoveSpeed;
            health = gameManager.slimeMoveSpeed;
            attackSpeed = gameManager.slimeAttackSpeed;
            damage = gameManager.slimeDamage;
            speed = gameManager.slimeAttackSpeed;
            range = gameManager.slimeRange;
        }
    }
    
    // gameManager 연결
    public void SetGameManager(GameObject gameManager)
    {
        this.gameManager = gameManager.GetComponent<GameManager>();
        transform.SetParent(gameManager.transform);
    }
}