using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScript : MonoBehaviour
{
    private CameraMoveToObject MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GetComponent<CameraMoveToObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            MainCamera.enabled = true;
        };
    }
}
