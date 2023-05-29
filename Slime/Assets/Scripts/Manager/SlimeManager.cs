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
    [SerializeField] public float jellyCount;

    private GameObject _slimeObject;

    private void Start()
    {
        _slimeObject = GameObject.FindGameObjectWithTag("Slime");
        Camera cam = Camera.main;
        cam.transform.SetParent(_slimeObject.transform);
    }

    public void AddHealth(float value)
    {
        health += value;

        if (health <= 0)
        {
            // GameOver
            health = 0;
        }

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }
}