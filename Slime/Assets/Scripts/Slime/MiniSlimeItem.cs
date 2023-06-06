using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlimeItem : MonoBehaviour
{
    [SerializeField] public float remainTime;
    private float _currentTime;
    
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (remainTime <= _currentTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetValues(float remainTime)
    {
        this.remainTime = remainTime;
    }
}
