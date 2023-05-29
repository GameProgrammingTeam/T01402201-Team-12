using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mapRendererPrefab;
    [SerializeField] private Vector3 position;
    [SerializeField] private int mapSize;
    [SerializeField] private Sprite mapTop;
    [SerializeField] private Sprite mapMiddle;
    [SerializeField] private Sprite mapBottom;

    public float width;
    public float height;

    void Start()
    {
        CreateMap();
    }

    void CreateMap()
    {
        width = mapMiddle.bounds.size.y;
        float totalHeight = 0;
        totalHeight += (mapSize > 0) ? mapTop.bounds.size.y : 0;
        totalHeight += (mapSize > 2) ? mapMiddle.bounds.size.y * (mapSize - 2) : 0;
        totalHeight += (mapSize > 1) ? mapBottom.bounds.size.y : 0;
        float prevHeight = position.y + (totalHeight - mapTop.bounds.size.y) / 2;
        height = prevHeight;
        Sprite sprite;
        for (int i = 0; i < mapSize; i++)
        {
            if (i == 0)
            {
                sprite = mapTop;
            }
            else if (i == mapSize - 1)
            {
                sprite = mapBottom;
            }
            else
            {
                sprite = mapMiddle;
            }

            Quaternion rotation = Quaternion.Euler(Vector3.zero);
            Vector3 rendererPosition = position + new Vector3(0, prevHeight, 0);
            SpriteRenderer mapRenderer = Instantiate(mapRendererPrefab, rendererPosition, rotation);
            mapRenderer.transform.SetParent(gameObject.transform);
            mapRenderer.sprite = sprite;
            prevHeight -= sprite.bounds.size.y;
        }
    }

    public void SetField(SpriteRenderer mapRendererPrefab,
        Vector3 position,
        int mapSize,
        Sprite mapTop,
        Sprite mapMiddle,
        Sprite mapBottom)
    {
        this.mapRendererPrefab = mapRendererPrefab;
        this.position = position;
        this.mapSize = mapSize;
        this.mapTop = mapTop;
        this.mapMiddle = mapMiddle;
        this.mapBottom = mapBottom;
    }
}