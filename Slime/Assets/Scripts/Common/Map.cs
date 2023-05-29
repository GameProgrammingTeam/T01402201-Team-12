using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = System.Numerics.Vector2;

public class Map : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mapRendererPrefab;
    [SerializeField] private Vector3 position;
    [SerializeField] private int mapSize;
    [SerializeField] private Sprite mapTop;
    [SerializeField] private Sprite mapMiddle;
    [SerializeField] private Sprite mapBottom;

    void Start()
    {
        float totalHeight = 0;
        totalHeight += (mapSize > 0) ? mapTop.bounds.size.y : 0;
        totalHeight += (mapSize > 2) ? mapMiddle.bounds.size.y * (mapSize - 2) : 0;
        totalHeight += (mapSize > 1) ? mapBottom.bounds.size.y * (mapSize - 2) : 0;
        float prevHeight = (totalHeight - mapTop.bounds.size.y) / 2;
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
}