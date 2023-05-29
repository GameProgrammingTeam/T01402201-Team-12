using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    [SerializeField] public SlimeSet slime;
    [SerializeField] public Attacker attackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float health;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;
}
