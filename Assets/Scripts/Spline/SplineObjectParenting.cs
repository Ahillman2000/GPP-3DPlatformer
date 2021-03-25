using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineObjectParenting : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    public bool playerOnPlatform = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other == playerCollider)
        {
            Debug.Log("player parented to platform");
            playerOnPlatform = true;
            player.transform.parent = this.transform;
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other == playerCollider)
        {
            playerOnPlatform = true;
            player.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            playerOnPlatform = false;
            player.transform.parent = null;
        }
    }
}
