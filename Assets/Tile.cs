using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool scored;
    private void OnEnable()
    {
        scored = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && false == scored)
        {
            GameManager.Instance.score++;
            scored = true;
        }
    }
}
