using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSlimeManager : MonoBehaviour
{
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
    [SerializeField] public float range;
}
