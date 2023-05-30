using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    [SerializeField] public SlimeSet slime;
    [SerializeField] public Attacker attackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public float immuneTime;
    [SerializeField] public bool immune;
    [SerializeField] public int exp;

    private GameManager _gameManager;
    private GameObject _slimeObject;

    private void Start()
    {
        _gameManager = gameObject.GetComponent<GameManager>();
        _slimeObject = GameObject.FindGameObjectWithTag("Slime");
        Camera cam = Camera.main;
        cam.transform.SetParent(_slimeObject.transform);
        health = maxHealth;
    }

    public void AddHealth(float value)
    {
        health += value;

        if (health <= 0)
        {
            // GameOver
            health = 0;
            Time.timeScale = 0.0f;
        }

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void AddExp(int value)
    {
        exp += value;
    }
}