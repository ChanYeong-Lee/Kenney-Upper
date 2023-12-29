using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public Transform backgroundsParent;
    private Transform player;
    public float generateHeight;
    public GameObject backgroundPrefab;
    public Transform curBackground;
    private Transform prevBackground;

    private float prefabHeight;
    private float halfHeight;

    private void Awake()
    {
        prefabHeight = backgroundPrefab.transform.Find("Vertical1").GetComponent<SpriteRenderer>().size.y;
        halfHeight = prefabHeight / 2;
        player = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        float playerMinY = player.position.y - generateHeight;
        float playerMaxY = player.position.y + generateHeight;
        if (prevBackground != null)
        {
            if (prevBackground.position.y + halfHeight > playerMinY || prevBackground.position.y - halfHeight < playerMaxY)
            {
                Destroy(prevBackground.gameObject);
                prevBackground = null;    
            }
        }
        else
        {
            if (curBackground.position.y + halfHeight < playerMaxY)
            {
                prevBackground = curBackground;
                curBackground = Instantiate(backgroundPrefab, (Vector2)prevBackground.position + new Vector2(0, prefabHeight), Quaternion.identity).transform;
            }
            else if(curBackground.position.y - halfHeight > playerMinY)
            {
                prevBackground = curBackground;
                curBackground = Instantiate(backgroundPrefab, (Vector2)prevBackground.position - new Vector2(0, prefabHeight), Quaternion.identity).transform;
            }
        }
    }

}
