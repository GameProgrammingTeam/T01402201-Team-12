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
    [SerializeField] public bool randomDamage;
    [SerializeField] public float minDamage;
    [SerializeField] public float damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float destroyRange;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        
        float spawnerHalfWidth = virusSlimeSpawnerPrefab.transform.lossyScale.x / 2;
        float halfWidth = _camera.orthographicSize * _camera.aspect;
        Vector3 position = new Vector3(halfWidth + spawnerHalfWidth + weight, 0, 0);
        Quaternion rotation = Quaternion.Euler(Vector3.zero);

        VirusSlimeSpawner virusSlimeSpawnerLeft = Instantiate(virusSlimeSpawnerPrefab, -position, rotation);
        virusSlimeSpawnerLeft.SetVirusSlimeManager(gameObject);
        virusSlimeSpawnerLeft.transform.SetParent(_camera.transform);
        VirusSlimeSpawner virusSlimeSpawnerRight = Instantiate(virusSlimeSpawnerPrefab, position, rotation);
        virusSlimeSpawnerRight.SetVirusSlimeManager(gameObject);
        virusSlimeSpawnerRight.transform.SetParent(_camera.transform);
    }
}