using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] public Attack attackPrefeb;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    
    private Slime slime;
    private Camera camera;

    private float currentTime;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        SetValues();

        currentTime += Time.deltaTime;
        if (currentTime > (1 / attackSpeed))
        {
            CreateAttack();
            currentTime = 0;
        }
    }

    void CreateAttack()
    {
        Vector2 mouseDirection = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = transform.position;
        Vector2 attackDirection = (mouseDirection - currentPosition).normalized;

        Vector3 attackPosition = new Vector3(currentPosition.x, currentPosition.y, 0);
        Quaternion rotation = Quaternion.Euler(Vector3.zero);
        Attack attack = Instantiate(attackPrefeb, attackPosition, rotation);
        attack.SetField(attackPosition, attackDirection, damage, speed, range);
    }

    // slime의 값 변경시 적용
    void SetValues()
    {
        if (!ReferenceEquals(slime, null))
        {
            attackPrefeb = slime.attackPrefab;
            attackSpeed = slime.attackSpeed;
            damage = slime.damage;
            speed = slime.speed;
            range = slime.range;
        }
    }

    // slime 연결
    public void SetSlime(GameObject slimeObject)
    {
        slime = slimeObject.GetComponent<Slime>();
        transform.SetParent(slimeObject.transform, false);
    }
}