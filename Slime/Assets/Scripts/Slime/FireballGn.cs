using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGn : MonoBehaviour
{
    [SerializeField] public Fireball FireballPrefab;
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
        if (_currentTime > (15 / attackSpeed))
        {
            CreateFireball();
            _currentTime = 0;
        }
    }
    void CreateFireball()
    {
        Vector2 mouseDirection = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = transform.position;
        Vector2 attackDirection = (mouseDirection - currentPosition).normalized;

        Vector3 attackPosition = new Vector3(currentPosition.x, currentPosition.y, 0);
        Quaternion rotation = Quaternion.Euler(Vector3.zero);
        Fireball fireball = Instantiate(FireballPrefab, attackPosition, rotation);
        fireball.SetField(attackPosition, attackDirection, damage, speed, range);
    }


    // slime의 값 변경시 적용
    void SetValues()
    {
        if (!ReferenceEquals(_slime, null))
        {
            FireballPrefab = _slime.FireballPrefab;
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
