using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughPlatform : MonoBehaviour
{
    private Tile platform;
    private void Awake()
    {
        platform = GetComponentInParent<Tile>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            platform.gameObject.layer = LayerMask.NameToLayer("Throughing");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            platform.gameObject.layer = LayerMask.NameToLayer("Platform");
        }
    }
}
