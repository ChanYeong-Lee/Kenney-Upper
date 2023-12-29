using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughPlatform : MonoBehaviour
{
    private GameObject platform;
    private void Awake()
    {
        platform = GetComponentInParent<GameObject>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            platform.layer = LayerMask.NameToLayer("Throughing");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            platform.layer = LayerMask.NameToLayer("Platform");
        }
    }
}
