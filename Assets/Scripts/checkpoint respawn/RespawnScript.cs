using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    //
    CheckpointManager checkpointManager;

    public GameObject spawnPoint;
    public float deathDepth = 0;

    void Start()
    {
        checkpointManager = GameObject.Find("Scene Manager").GetComponent<CheckpointManager>();
        this.transform.position = spawnPoint.transform.position;
    }

    void Respawn()
    {
        if(checkpointManager.currentCheckpoint == null)
        {
            this.transform.position = spawnPoint.transform.position;
        }
        else
        {
            this.transform.position = checkpointManager.currentCheckpoint.transform.position;
        }
    }
    
    void Update()
    {
        if (this.transform.position.y < deathDepth || Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
}
