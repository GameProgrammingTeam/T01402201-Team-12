
using System;
using System.Collections.Generic;
using UnityEngine;
 
public class KeyInput : MonoBehaviour
{

    Dictionary<KeyCode, Action> keyDictionary;
    public GameObject PauseObject;

    void Start()
    {
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Escape, KeyDown_Escape },
            { KeyCode.B, KeyDown_B },
            { KeyCode.C, KeyDown_C }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var dic in keyDictionary)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }

    }

    private void KeyDown_Escape()
    {
        PauseObject.SetActive(true);
        Time.timeScale = 0;
    }
    private void KeyDown_B()
    {
        Debug.Log("B");
    }
    private void KeyDown_C()
    {
        Debug.Log("C");
    }
}

