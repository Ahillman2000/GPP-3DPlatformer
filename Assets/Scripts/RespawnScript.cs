using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject respawnPoint;
    public float deathDepth = 0;

    void Start()
    {
        this.transform.position = respawnPoint.transform.position;
    }

    void Respawn()
    {
        this.transform.position = respawnPoint.transform.position;
    }
    
    void Update()
    {
        if (this.transform.position.y < deathDepth || Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
}
