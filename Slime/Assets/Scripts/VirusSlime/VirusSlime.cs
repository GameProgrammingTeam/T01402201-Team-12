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
    [SerializeField] private Jelly jellyPrefab;
    [SerializeField] private int jellyExp;
    [SerializeField] private float jellyProbability;
    [SerializeField] private float jellyRemainTime;
    [SerializeField] private GameObject impactEffectPrefab;
    [SerializeField] private GameObject ElectricballHitPrefab;
    [SerializeField] private GameObject FireballHitPrefab;

    [SerializeField] private MiniSlimeItem miniSlimeItemPrefab;
    [SerializeField] private float miniSlimeItemProbability;
    [SerializeField] private float miniSlimeItemRemainTime;
    
    private Camera _camera;
    private GameObject player;
    private SlimeMovement movement;

    public ParticleSystem ps;
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
    public LayerMask layerMask;
    public GameObject Explosion;


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

        //ps = GameObject.FindWithTag("Fireball").GetComponent<ParticleSystem>();
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

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Attackpop);

            Destroy(effect, 1f);


            if (health <= 0)
            {
                CreateJelly();
                CreateMiniSlimeItem();
                Destroy(gameObject);
                
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Electricball"))
        {
            float damage = other.gameObject.GetComponent<Electricball>().damage;
            health -= damage;

            Vector3 pos = Camera.main.WorldToScreenPoint(other.transform.position);
            DamageTextController.Instance.CreateDamageText(pos, damage);
            GameObject effect = Instantiate(ElectricballHitPrefab, transform.position, Quaternion.identity);

            
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Spark);

            Destroy(effect, 1f);



            if (health <= 0)
            {
                CreateJelly();
                CreateMiniSlimeItem();
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Fireball"))
        {
            float damage = other.gameObject.GetComponent<Fireball>().damage * 1.2f;
            health -= damage;

            Vector3 pos = Camera.main.WorldToScreenPoint(other.transform.position);
            DamageTextController.Instance.CreateDamageText(pos, damage);
            GameObject effect = Instantiate(FireballHitPrefab, transform.position, Quaternion.identity);
            

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Fire);
            GameObject Explosioneffect = Instantiate(Explosion, this.transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            



            if (health <= 0)
            {
                CreateJelly();
                CreateMiniSlimeItem();
                Destroy(gameObject);
            }
        }



    }

    private void OnParticleCollision(GameObject other) // 구현 실패 코드
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            float damage = other.gameObject.GetComponent<Fireball>().damage;
            health -= damage;

            Vector3 pos = Camera.main.WorldToScreenPoint(other.transform.position);
            DamageTextController.Instance.CreateDamageText(pos, damage);
            GameObject effect = Instantiate(FireballHitPrefab, transform.position, Quaternion.identity);


            AudioManager.instance.PlaySfx(AudioManager.Sfx.Spark); // 

            Destroy(effect, 1f);



            if (health <= 0)
            {
                CreateJelly();
                CreateMiniSlimeItem();
                Destroy(gameObject);
            }
        }
    }

    private void OnParticleTrigger()
    {
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5, layerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody enemyRid = colliders[i].GetComponent<Rigidbody>();
            enemyRid.AddExplosionForce(100, transform.position, 5);
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
    
    private void CreateMiniSlimeItem()
    {
        if (Random.Range(0, 1.0f) <= miniSlimeItemProbability)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.zero);
            MiniSlimeItem miniSlimeItem = Instantiate(miniSlimeItemPrefab, transform.position, rotation);
            miniSlimeItem.SetValues(miniSlimeItemRemainTime);
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
        MiniSlimeItem miniSlimeItemPrefab,
        float miniSlimeItemProbability,
        float miniSlimeItemRemainTime,
        GameObject impactEffectPrefab,
        GameObject ElectricballHitPrefab,
        GameObject FireballHitPrefab,
        GameObject Explosion)
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
        this.miniSlimeItemPrefab = miniSlimeItemPrefab;
        this.miniSlimeItemProbability = miniSlimeItemProbability;
        this.miniSlimeItemRemainTime = miniSlimeItemRemainTime;
        this.impactEffectPrefab = impactEffectPrefab;
        this.ElectricballHitPrefab = ElectricballHitPrefab;
        this.FireballHitPrefab = FireballHitPrefab;
        this.Explosion = Explosion;
}
}