using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlime : MonoBehaviour
{
    [SerializeField] private SlimeManager slimeManager;

    [SerializeField] private SlimeSet slime;
    [SerializeField] private Slime parentSlime;
    [SerializeField] private Attacker attackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public float distance = 1.0f;

    private Animator _animator;
    private Attacker _attacker;
    private SlimeMovement _movement;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<SlimeMovement>();
        UpdateAssets();
        CreateAttacker();
    }

    void Update()
    {
        SetValues();
        Vector2 currentPosition = transform.position;
        Vector2 slimePosition = parentSlime.transform.position;
        
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
        _attacker = Instantiate(attackerPrefab);
        _attacker.transform.localScale = Vector3.one;
        _attacker.SetSlime(gameObject);
    }

    // gameManager의 값 변경시 적용
    void SetValues()
    {
        if (!ReferenceEquals(slimeManager, null))
        {
            if (slimeManager.slime != slime)
            {
                slime = slimeManager.slime;
                UpdateAssets();
            }

            attackerPrefab = slimeManager.attackerPrefab;
            attackPrefab = slimeManager.attackPrefab;
            moveSpeed = slimeManager.moveSpeed;
            attackSpeed = slimeManager.attackSpeed;
            damage = slimeManager.damage;
            speed = slimeManager.speed;
            range = slimeManager.range;
        }
    }

    // gameManager 연결
    public void SetParentSlime(Slime parent)
    {
        this.parentSlime = parent;
        transform.SetParent(parent.transform);
    }
}