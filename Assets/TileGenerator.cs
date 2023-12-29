using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private Player player;

    public Transform tileParent;
    public GameObject[] tilePrefabs;

    private Transform prevHeight;

    private Queue<GameObject> tileQueue = new Queue<GameObject>();
    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    private void GenerateTile()
    {
        float posX = Random.Range(-3f, 3f);
        float posY = player.transform.position.y + Random.Range(1, player.jumpHeight - 0.2f);
        GameObject newTile;
        if (tileQueue.Count < 1)
        {
            newTile = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)]);
            newTile.transform.parent = tileParent;
        }
        else
        {
            newTile = tileQueue.Dequeue();
            newTile.SetActive(true);
        }
        newTile.transform.position = new Vector2(posX, posY);
        prevHeight = newTile.transform;
    }

    private void RemoveTile()
    {
        if (player.transform.position.y > prevHeight.position.y + 15)
        {
            prevHeight.gameObject.SetActive(false);
            tileQueue.Enqueue(prevHeight.gameObject);
        }
    }

}
