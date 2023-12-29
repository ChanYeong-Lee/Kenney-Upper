    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public Transform parent;
    private Transform player;
    public float playerPos;
    public float margin;
    public GameObject prefab;
    public Transform cur;
    public Transform prev;
    public Transform next;

    public float prefabHeight;
    public float halfHeight;

    private void Awake()
    {
        prefabHeight = prefab.transform.Find("Vertical1").GetComponent<SpriteRenderer>().size.y;
        halfHeight = prefabHeight / 2;
    }

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        playerPos = player.transform.position.y;
        if (player.transform.position.y > maxMargin(cur.position.y))
        {
            if (next == null)
            {
                next = Instantiate(prefab, new Vector2(0, cur.position.y + prefabHeight), Quaternion.identity).transform;
                next.parent = parent;
            }
        }
        if (player.transform.position.y < minMargin(cur.position.y))
        {
            if (prev == null)
            {
                prev = Instantiate(prefab, new Vector2(0, cur.position.y - prefabHeight), Quaternion.identity).transform;
                prev.parent = parent;
            }
        }

        if (next != null)
        {
            if (player.transform.position.y > minMargin(next.position.y))
            {
                Destroy(cur.gameObject);
                cur = next;
                next = null;
            }
        }
        if (prev != null)
        {
            if (player.transform.position.y < maxMargin(prev.position.y))
            {
                Destroy(cur.gameObject);
                cur = prev;
                prev = null;
            }
        }
    }

    private float maxMargin(float posY)
    {
        return posY + halfHeight - margin;
    }
    private float minMargin(float posY)
    {
        return posY - halfHeight + margin;
    }
}
