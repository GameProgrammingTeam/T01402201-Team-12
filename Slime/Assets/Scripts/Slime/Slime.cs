using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private SlimeManager slimeManager;

    [SerializeField] private SlimeSet slime;
    [SerializeField] private Attacker attackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;

    [SerializeField] public float immuneTime;
    [SerializeField] public bool immune;
    
    private float _currentImmuneTime;

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
        if (immune)
        {
            _currentImmuneTime += Time.deltaTime;
            if (immuneTime <= _currentImmuneTime)
            {
                immune = false;
            }
        }

        Vector2 currentPosition = transform.position;
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(dirX, dirY).normalized * moveSpeed;
        _movement.MoveTo(currentPosition, move);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("VirusSlime"))
        {
            if (!immune)
            {
                float damage = other.gameObject.GetComponent<VirusSlime>().damage;
                AddHealth(-damage);
                immune = true;
            }
        }

        if (other.gameObject.CompareTag("Jelly"))
        {
            int exp = other.gameObject.GetComponent<Jelly>().exp;
            AddExp(exp);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("MiniSlimeItem"))
        {
            AddMiniSlime(other.gameObject.transform);
            Destroy(other.gameObject);
        }
    }

    private void AddHealth(float value)
    {
        if (!ReferenceEquals(slimeManager, null))
        {
            slimeManager.AddHealth(value);
        }
        else
        {
            health += value;
            if (health >= maxHealth)
            {
                health = maxHealth;
            }

            if (health <= 0)
            {
                health = 0;
            }
        }
    }

    private void AddExp(int value)
    {
        if (!ReferenceEquals(slimeManager, null))
        {
            slimeManager.AddExp(value);
        }
    }

    private void AddMiniSlime(Transform transform)
    {
        if (!ReferenceEquals(slimeManager, null))
        {
            slimeManager.AddMiniSlime(transform);
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
            health = slimeManager.health;
            maxHealth = slimeManager.maxHealth;
            attackSpeed = slimeManager.attackSpeed;
            damage = slimeManager.damage;
            speed = slimeManager.speed;
            range = slimeManager.range;
            immuneTime = slimeManager.immuneTime;
            immune = slimeManager.immune;
        }
    }

    // gameManager 연결
    public void SetSlimeManager(GameObject slimeManager)
    {
        this.slimeManager = slimeManager.GetComponent<SlimeManager>();
        transform.SetParent(slimeManager.transform);
    }
}