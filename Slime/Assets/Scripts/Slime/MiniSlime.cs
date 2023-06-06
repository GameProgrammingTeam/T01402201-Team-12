using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlime : MonoBehaviour
{
    [SerializeField] private SlimeManager slimeManager;

    [SerializeField] private SlimeSet slime;
    [SerializeField] private MiniSlimeAttacker miniSlimeAttackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public float distance = 1.0f;

    private GameObject player;
    private Animator _animator;
    private MiniSlimeAttacker _attacker;
    private SlimeMovement _movement;

    void Start()
    {
        player = GameObject.FindWithTag("Slime");
        _animator = GetComponent<Animator>();
        _movement = GetComponent<SlimeMovement>();
        UpdateAssets();
        CreateAttacker();
    }

    void Update()
    {
        SetValues();
        Vector2 currentPosition = transform.position;
        Vector2 slimePosition = player.transform.position;
        
        if (Vector2.Distance(currentPosition, slimePosition) > distance)
        {
            Vector2 move = (slimePosition - currentPosition).normalized * moveSpeed;
            _movement.MoveTo(currentPosition, move);
        }
    }

    // 슬라임 외형 변경
    void UpdateAssets()
    {
        var resource = "SlimeAnimation/SlimeAnimationOverrideController/" + slime;
        _animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(resource);
    }

    // Attacker 생성
    void CreateAttacker()
    {
        _attacker = Instantiate(miniSlimeAttackerPrefab);
        _attacker.transform.localScale = Vector3.one;
        _attacker.SetSlime(gameObject);
    }

    // gameManager의 값 변경시 적용
    void SetValues()
    {
        if (!ReferenceEquals(slimeManager, null))
        {
            if (slimeManager.miniSlime != slime)
            {
                slime = slimeManager.miniSlime;
                UpdateAssets();
            }
            
            miniSlimeAttackerPrefab = slimeManager.miniSlimeAttackerPrefab;
            attackPrefab = slimeManager.miniSlimeAttackPrefab;
            moveSpeed = slimeManager.miniSlimeMoveSpeed;
            attackSpeed = slimeManager.miniSlimeAttackSpeed;
            damage = slimeManager.miniSlimeDamage;
            speed = slimeManager.miniSlimeSpeed;
            range = slimeManager.miniSlimeRange;
            distance = slimeManager.miniSlimeDistance;
        }
    }

    // gameManager 연결
    public void SetSlimeManager(GameObject slimeManager)
    {
        print( slimeManager.GetComponent<SlimeManager>());
        this.slimeManager = slimeManager.GetComponent<SlimeManager>();
        SetValues();
    }
}