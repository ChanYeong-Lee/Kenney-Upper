using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeight : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = GetComponentInParent<Player>().transform;
    }
    private void Update()
    {
        transform.position = new Vector2(0, player.position.y);
    }
    
}
