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
    [SerializeField] private Jelly jellyPrefab;
    [SerializeField] private int jellyExp;
    [SerializeField] private float jellyProbability;
    [SerializeField] private float jellyRemainTime;
    [SerializeField] private GameObject impactEffectPrefab;

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

            Vector3 pos = Camera.main.WorldToScreenPoint(other.transform.position);
            DamageTextController.Instance.CreateDamageText(pos, damage);
            GameObject effect = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1f);


            if (health <= 0)
            {
                CreateJelly();
                Destroy(gameObject);
                
            }

            Destroy(other.gameObject);
        }
    }

    private void CreateJelly()
    {
        if (Random.Range(0, 1.0f) <= jellyProbability)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.zero);
            Jelly jelly = Instantiate(jellyPrefab, transform.position, rotation);
            jelly.SetValues(jellyRemainTime, jellyExp);
        }
    }

    public void SetField(
        SlimeSet slime,
        float moveSpeed,
        float health,
        float maxHealth,
        float damage,
        float attackSpeed,
        float destroyRange,
        Jelly jellyPrefab,
        int jellyExp,
        float jellyProbability,
        float jellyRemainTime,
        GameObject impactEffectPrefab)
    {
        this.slime = slime;
        this.moveSpeed = moveSpeed;
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.destroyRange = destroyRange;
        this.jellyPrefab = jellyPrefab;
        this.jellyExp = jellyExp;
        this.jellyProbability = jellyProbability;
        this.jellyRemainTime = jellyRemainTime;
        this.impactEffectPrefab = impactEffectPrefab;
    }
}