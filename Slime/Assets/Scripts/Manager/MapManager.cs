using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Map mapPrefab;
    [SerializeField] private int mapSize;
    [SerializeField] private Sprite mapTop;
    [SerializeField] private Sprite mapMiddle;
    [SerializeField] private Sprite mapBottom;

    private Camera camera;
    private List<Map> maps = new List<Map>();

    private void Start()
    {
        camera = Camera.main;
        Quaternion rotation = Quaternion.Euler(Vector3.zero);
        Map map = Instantiate(mapPrefab, camera.transform.position, rotation);
        maps = maps.Append(map).ToList();
    }

    void Update()
    {
        MapManagement();
    }

    void MapManagement()
    {
        Transform cameraTransform = camera.transform;

        float cameraLeft = cameraTransform.position.x - camera.sensorSize.x / 2;
        float cameraRight = cameraTransform.position.x + camera.sensorSize.x / 2;
        
        List<Map> newMaps = new List<Map>();
        for (int i = 0; i < maps.Count; i++)
        {
            float mapLeft = maps[i].transform.position.x - maps[i].width / 2;
            float mapRight = maps[i].transform.position.x + maps[i].width / 2;
            if ((mapLeft <= cameraLeft && cameraLeft <= mapRight) ||
                (mapLeft <= cameraRight && cameraRight <= mapRight) ||
                (cameraLeft <= mapLeft && mapLeft <= cameraRight) || 
                (cameraLeft <= mapRight && mapRight <= cameraRight))
            {
                newMaps = newMaps.Append(maps[i]).ToList();
            }
            else
            {
                Destroy(maps[i].gameObject);
            }

            maps = newMaps;
        }
    }

    void SetCamera(Camera camera)
    {
        this.camera = camera;
    }
}