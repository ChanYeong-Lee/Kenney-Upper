using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile prevTile;
    public Tile nextTile;
    private Player player;
    private bool scored;
    private void Start()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        float distance = transform.position.y - player.transform.position.y;
        float randomX = Random.Range(-player.jumpSpeed + 0.5f, player.jumpSpeed - 0.5f);
        float randomY = Random.Range(1, player.jumpHeight - 0.5f);
        if (prevTile == null)
        {
            if (distance < -20)
            {
                TileGenerator.Instance.RemoveTile(this.gameObject);
                if(nextTile!=null)
                nextTile.prevTile = null;
                nextTile = null;
            }
        }
        if (nextTile == null)
        {
            if (distance > 30)
            {
                TileGenerator.Instance.RemoveTile(this.gameObject);
                if(prevTile!=null)
                prevTile.nextTile = null;
                prevTile = null;
            }
        }
        if (distance < 15)
        {
            if (nextTile == null)
            {
                nextTile = TileGenerator.Instance.GenerateTile(new Vector2(randomX, transform.position.y + randomY)).GetComponent<Tile>();
                nextTile.prevTile = this;
            }
        }
        if (distance > -10)
        {
            if (prevTile == null)
            {
                prevTile = TileGenerator.Instance.GenerateTile(new Vector2(randomX, transform.position.y - randomY)).GetComponent<Tile>();
                prevTile.nextTile = this;
            }
        }
        if (prevTile == this || nextTile == this)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameManager.Instance.SetScore(transform.position.y);
        }
    }
}
