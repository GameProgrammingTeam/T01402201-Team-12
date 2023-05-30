using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Input = UnityEngine.Input;

public class GameStart : MonoBehaviour
{ 
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
                {
            Invoke("InputGameStart", 6f);
            //obj.GetComponent<GameManager>().InputGameStart();
        };
       
        
    }
    
}
