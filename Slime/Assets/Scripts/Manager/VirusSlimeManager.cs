using UnityEngine;

public class VirusSlimeManager : MonoBehaviour
{
    [SerializeField] public VirusSlimeSpawner virusSlimeSpawnerPrefab;
    [SerializeField] public float weight;
    [SerializeField] public float spawnTime;
    [SerializeField] public int spawnMaxCount;
    [SerializeField] public SlimeProbability[] probabilities;
    [SerializeField] public VirusSlime virusSlimePrefab;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public bool randomDamage;
    [SerializeField] public float minDamage;
    [SerializeField] public float damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float destroyRange;
    [SerializeField] public Jelly jellyPrefab;
    [SerializeField] public int jellyExp;
    [SerializeField] public float jellyProbability;
    [SerializeField] public float jellyRemainTime;
    
    [SerializeField] public MiniSlimeItem miniSlimeItemPrefab;
    [SerializeField] public float miniSlimeItemProbability;
    [SerializeField] public float miniSlimeItemRemainTime;
    
    [SerializeField] public GameObject impactEffectPrefab;
    [SerializeField] public GameObject ElectricballHitPrefab;

    private VirusSlimeSpawner virusSlimeSpawnerLeft;
    private VirusSlimeSpawner virusSlimeSpawnerRight;
    

    
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        float spawnerHalfWidth = virusSlimeSpawnerPrefab.transform.lossyScale.x / 2;
        float halfWidth = _camera.orthographicSize * _camera.aspect;
        Vector3 position = new Vector3(halfWidth + spawnerHalfWidth + weight, 0, 0);
        Quaternion rotation = Quaternion.Euler(Vector3.zero);

        virusSlimeSpawnerLeft = Instantiate(virusSlimeSpawnerPrefab, -position, rotation);
        virusSlimeSpawnerLeft.SetVirusSlimeManager(gameObject);
        virusSlimeSpawnerLeft.transform.SetParent(_camera.transform);
        virusSlimeSpawnerRight = Instantiate(virusSlimeSpawnerPrefab, position, rotation);
        virusSlimeSpawnerRight.SetVirusSlimeManager(gameObject);
        virusSlimeSpawnerRight.transform.SetParent(_camera.transform);
        if (jellyProbability > 1.0f)
        {
            jellyProbability = 1.0f;
        }

        if (jellyProbability < 0.0f)
        {
            jellyProbability = 0.0f;
        }
    }

    public void Upgrade()
    {
        maxHealth *= 1.1f;;
        damage *= 1.1f;
    }
}