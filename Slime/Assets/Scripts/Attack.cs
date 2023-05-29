using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public Vector2 startPosition;
    [SerializeField] public float damage;
    [SerializeField] public Vector2 direction;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    
    void Update()
    {
        Vector2 move = direction * speed;
        transform.position += new Vector3(move.x, move.y, 0) * Time.deltaTime;
        
        float currentRange = Vector2.Distance(startPosition, transform.position);
        if (currentRange >= range)
        {
            OutOfRange();
        }
    }

    public void SetField(Vector2 startPosition, Vector2 direction, float damage, float speed, float range)
    {
        this.startPosition = startPosition;
        this.damage = damage;
        this.direction = direction;
        this.speed = speed;
        this.range = range;
    }

    void OutOfRange()
    {
        Destroy(gameObject);
    }
}