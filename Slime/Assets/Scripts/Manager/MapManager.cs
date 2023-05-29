using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private int mapSize;
    [SerializeField] private Sprite mapBottom;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        // print(camera.transform.position.x);
        // if (camera.transform.position.x < 0)
        // {
        //     Quaternion rotation = Quaternion.Euler(Vector3.zero);
        //     Instantiate(mapBottom, camera.transform.position, rotation);
        // }
    }

    void SetCamera(Camera camera)
    {
        this.camera = camera;
    }
}
