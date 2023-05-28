using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class VirusSlimeSpawner : MonoBehaviour
{
    [Serializable]
    public struct SlimeProbability
    {
        public SlimeProbability(SlimeSet slime, float probability)
        {
            this.slime = slime;
            this.probability = probability;
        }

        public SlimeSet slime;
        public float probability;
    }

    [SerializeField] private float spawnTime;
    [SerializeField] private int spawnMaxCount;
    [SerializeField] private SlimeProbability[] probabilities;
    [SerializeField] private VirusSlime virusSlimePrefab;

    [SerializeField] private GameObject positionObject;
    [SerializeField] private float position;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float range;

    private float currentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        float probabilitySum = 0.0f;
        foreach (var slimeProbability in probabilities)
        {
            probabilitySum += slimeProbability.probability;
        }

        probabilitySum = (probabilitySum > 0) ? probabilitySum : 1;
        for (int i = 0; i < probabilities.Length; i++)
        {
            probabilities[i] = new SlimeProbability(probabilities[i].slime,
                (probabilities[i].probability / probabilitySum));
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > spawnTime)
        {
            Spawn();
            currentTime = 0;
        }
    }

    void Spawn()
    {
        for (int i = 0; i < Random.Range(0, spawnMaxCount + 1); i++)
        {
            SlimeSet slime = Catch().slime;
            CreateVirusSlime(slime);
        }
    }

    SlimeProbability Catch()
    {
        float prev = 0.0f;
        float choice = Random.Range(0, 10000);
        foreach (var slimeProbability in probabilities)
        {
            prev += slimeProbability.probability * 10000;
            if (choice <= prev)
            {
                return slimeProbability;
            }
        }

        return probabilities[0];
    }

    void CreateVirusSlime(SlimeSet slimeSet)
    {
        Vector2 currentPosition = transform.position;
        float widthRange = transform.lossyScale.x / 2;
        float heightRange = transform.lossyScale.y / 2;
        Vector3 randomPosition = new Vector3(Random.Range(-widthRange, widthRange),
            Random.Range(-heightRange, heightRange),
            0);
        Vector3 slimePosition = new Vector3(currentPosition.x, currentPosition.y, 0) + randomPosition;
        Quaternion rotation = Quaternion.Euler(Vector3.zero);
        VirusSlime virusSlime = Instantiate(virusSlimePrefab, slimePosition, rotation);
        virusSlime.SetField(slimeSet, moveSpeed, health, damage, attackSpeed, range);
    }
}