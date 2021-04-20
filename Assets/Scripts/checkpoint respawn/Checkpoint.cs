using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CheckpointManager checkpointManager;

    GameObject player;
    Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = GameObject.Find("Scene Manager").GetComponent<CheckpointManager>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == playerCollider)
        {
            checkpointManager.currentCheckpoint = this.gameObject;
        }
    }
}
