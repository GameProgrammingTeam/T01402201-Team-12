using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class DamageText : MonoBehaviour
{
    private Position startPosition;
    private float maxRange = 10.0f;
    private float range = 0.0f;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 nextPos = Vector2.up * 20 * Time.deltaTime;
        range += nextPos.y;
        transform.position += nextPos;
        if (range >= maxRange)
        {
            DestroyEvent();
        }
    }

    public void DestroyEvent()
    {
        Destroy(gameObject);
    }

    public void SetRange(float range)
    {
        maxRange = range;
    }
}
