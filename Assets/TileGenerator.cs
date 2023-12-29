using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public static TileGenerator Instance { get; private set; }
    private Player player;

    public Transform tileParent;
    public GameObject[] tilePrefabs;

    private Transform prevHeight;

    private Queue<GameObject> tileQueue = new Queue<GameObject>();
    private void Awake()
    {
        Instance = this;
        player = GameManager.Instance.player;
    }

    public GameObject GenerateTile(Vector2 pos)
    {
        GameObject newTile;
        if (tileQueue.Count > 0)
        {
            newTile = tileQueue.Dequeue();
        }
        else
        {
            newTile = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)]);
        }
        newTile.transform.parent = tileParent;
        newTile.transform.position = pos;
        newTile.SetActive(true);

        return newTile;
    }

    public void RemoveTile(GameObject tile)
    {
        tile.SetActive(false);
        tileQueue.Enqueue(tile);
    }
   
}
