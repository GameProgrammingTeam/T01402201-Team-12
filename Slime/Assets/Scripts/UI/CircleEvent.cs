using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEvent : MonoBehaviour
{
    public GameObject GameoverObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameoverEvent()
    {
        Time.timeScale = 0.0f;
        GameoverObj.SetActive(true);
    }
}
