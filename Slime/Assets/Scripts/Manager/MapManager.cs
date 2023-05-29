using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] public Map mapPrefab;
    [SerializeField] public SpriteRenderer mapRendererPrefab;
    [SerializeField] public int mapSize;
    [SerializeField] public Sprite mapTop;
    [SerializeField] public Sprite mapMiddle;
    [SerializeField] public Sprite mapBottom;

    private Camera _camera;

    private List<Map> _maps = new List<Map>();
    private float _width;

    private void Start()
    {
        _camera = Camera.main;
        _width = mapMiddle.bounds.size.x;
        CreateMap(0);
    }

    void Update()
    {
        MapManagement();
    }

    void MapManagement()
    {
        float cameraPositionX = _camera.transform.position.x;
        float cameraHalfWidth = _camera.orthographicSize * _camera.aspect;

        float cameraLeft = cameraPositionX - cameraHalfWidth;
        float cameraRight = cameraPositionX + cameraHalfWidth;

        List<Map> newMaps = new List<Map>();
        bool createMapLeft = true;
        float createMapLeftMax = 0.0f;
        bool createMapRight = true;
        float createMapRightMax = 0.0f;
        for (int i = 0; i < _maps.Count; i++)
        {
            float mapPositionX = _maps[i].transform.position.x;
            float mapHalfWidth = _maps[i].width / 2;
            float mapLeft = mapPositionX - mapHalfWidth;
            float mapRight = mapPositionX + mapHalfWidth;
            if ((mapLeft <= cameraLeft && cameraLeft <= mapRight) ||
                (mapLeft <= cameraRight && cameraRight <= mapRight) ||
                (cameraLeft <= mapLeft && mapLeft <= cameraRight) ||
                (cameraLeft <= mapRight && mapRight <= cameraRight))
            {
                newMaps = newMaps.Append(_maps[i]).ToList();
            }
            else
            {
                Destroy(_maps[i].gameObject);
            }

            if (mapPositionX <= createMapLeftMax)
                createMapLeftMax = mapPositionX;
            if (createMapRightMax <= mapPositionX)
                createMapRightMax = mapPositionX;

            if (mapLeft <= cameraLeft && cameraLeft <= mapRight)
            {
                createMapLeft = false;
            }

            if (mapLeft <= cameraRight && cameraRight <= mapRight)
            {
                // print(mapPositionX);
                createMapRight = false;
            }
        }
        _maps = newMaps;

        if (createMapLeft)
        {
            // print(createMapLeftMax - _width);
            CreateMap(createMapLeftMax - _width);
        }

        if (createMapRight)
        {
            // print(createMapRightMax + _width);
            CreateMap(createMapRightMax + _width);
        }
    }

    void CreateMap(float x)
    {
        Quaternion rotation = Quaternion.Euler(Vector3.zero);
        Map map = Instantiate(mapPrefab, new Vector3(x, 0, 0), rotation);
        map.SetField(mapRendererPrefab, new Vector2(x, 0), mapSize, mapTop, mapMiddle, mapBottom);
        _maps = _maps.Append(map).ToList();
    }
}