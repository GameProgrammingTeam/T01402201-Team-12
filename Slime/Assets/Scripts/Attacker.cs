using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public Attack attackPrefeb;

    private Camera camera;


    // Start is called before the first frame update
    private float currentTime = 0.0f;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
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
}