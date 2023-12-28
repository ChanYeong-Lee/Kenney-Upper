using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThroughablePlat : MonoBehaviour
{
    public GameObject platform;
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
