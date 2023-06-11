using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private SlimeManager slimeManager;

    [SerializeField] private SlimeSet slime;
    [SerializeField] private Attacker attackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] private ElectricballGn ElectricballGnPrefab;
    [SerializeField] public Electricball ElectricballPrefab;
    [SerializeField] private FireballGn FireballGnPrefab;
    [SerializeField] public Fireball FireballPrefab;



    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public SelectSlime selectslime;
    [SerializeField] public float immuneTime;
    [SerializeField] public bool immune;
    [SerializeField] public int PlayerID;
    private float _currentImmuneTime;
    [SerializeField] public int count;

    private Animator _animator;
    private Attacker _attacker;
    private ElectricballGn _ElectricballGn;
    private FireballGn _FireballGn;
    private SlimeMovement _movement;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<SlimeMovement>();
        
        UpdateAssets();
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
    public void UpdateAssets()
    {
        var resource = "SlimeAnimation/SlimeAnimationOverrideController/" + slime;
        _animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(resource);
    }

    // Attacker 생성
    public void CreateAttacker()
    {
        _attacker = Instantiate(attackerPrefab);
        _attacker.SetSlime(gameObject);
    }

    public void CreateElectricballGn()
    {
        _ElectricballGn = Instantiate(ElectricballGnPrefab);
        _ElectricballGn.SetSlime(gameObject);
    }

    public void CreateFireballGn()
    {
       _FireballGn = Instantiate(FireballGnPrefab);
       _FireballGn.SetSlime(gameObject);
    }
    // gameManager의 값 변경시 적용
    void SetValues()
    {
        if (!ReferenceEquals(slimeManager, null))
        {
                //slime = slimeManager.slime;
                switch (slimeManager.PlayerID)
                {
                    case 0:
                    slime = SlimeSet.Blue;
                    attackerPrefab = slimeManager.attackerPrefab;
                    attackPrefab = slimeManager.attackPrefab;
                    //PlayerID = slimeManager.GetComponent<SlimeManager>().PlayerID;
                    //CreateAttacker();
                    break;

                    case 18:
                    slime = SlimeSet.Fire;
                    FireballPrefab = slimeManager.FireballPrefab;
                    FireballGnPrefab = slimeManager.FireballGnPrefab;
                    break;

                    case 28:
                    slime = SlimeSet.Lightning;
                    ElectricballPrefab = slimeManager.ElectricballPrefab;
                    ElectricballGnPrefab = slimeManager.ElectricballGnPrefab;
                    //PlayerID = slimeManager.GetComponent<SlimeManager>().PlayerID;
                    //CreateElectricballGn();
                    break;

                    case 46:
                        slime = SlimeSet.Vine;
                        break;
                }
                
                UpdateAssets();
            

            
            moveSpeed = slimeManager.moveSpeed;
            health = slimeManager.health;
            maxHealth = slimeManager.maxHealth;
            attackSpeed = slimeManager.attackSpeed;
            damage = slimeManager.damage;
            speed = slimeManager.speed;
            range = slimeManager.range;
            immuneTime = slimeManager.immuneTime;
            immune = slimeManager.immune;
            count = slimeManager.count;
        }
    }

    // gameManager 연결
    public void SetSlimeManager(GameObject slimeManager)
    {
        this.slimeManager = slimeManager.GetComponent<SlimeManager>();
        transform.SetParent(slimeManager.transform);
    }
}