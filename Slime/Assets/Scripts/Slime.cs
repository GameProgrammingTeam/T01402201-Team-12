using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float damage;

    private SlimeMovement movement;


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<SlimeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(dirX, dirY).normalized * moveSpeed;
        print(move);
        movement.MoveTo(currentPosition, move);
    }
}