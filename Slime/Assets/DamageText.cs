using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{


 
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up;

    }

    public void DestroyEvent()
    {
        Destroy(gameObject);
    }
}
