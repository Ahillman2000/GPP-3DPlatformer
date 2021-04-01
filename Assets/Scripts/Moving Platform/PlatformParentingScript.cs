using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParentingScript : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == playerCollider)
        {
            player.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            player.transform.parent = null;
        }
    }
}
