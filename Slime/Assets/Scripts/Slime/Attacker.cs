using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Attack attackPrefeb;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    
    private Slime _slime;
    private Camera _camera;

    private float _currentTime;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        SetValues();

        _currentTime += Time.deltaTime;
        if (_currentTime > (1 / attackSpeed))
        {
            CreateAttack();
            _currentTime = 0;
        }
    }

    void CreateAttack()
    {
        Vector2 mouseDirection = _camera.ScreenToWorldPoint(Input.mousePosition);
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
        if (!ReferenceEquals(_slime, null))
        {
            attackPrefeb = _slime.attackPrefab;
            attackSpeed = _slime.attackSpeed;
            damage = _slime.damage;
            speed = _slime.speed;
            range = _slime.range;
        }
    }

    // slime 연결
    public void SetSlime(GameObject slimeObject)
    {
        _slime = slimeObject.GetComponent<Slime>();
        transform.SetParent(slimeObject.transform, false);
    }
}