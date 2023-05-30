using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] public float remainTime;
    [SerializeField] public int exp;
    private float _currentTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (remainTime <= _currentTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetValues(float remainTime, int exp)
    {
        this.remainTime = remainTime;
        this.exp = exp;
    }
}