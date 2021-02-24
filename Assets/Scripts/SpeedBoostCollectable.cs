using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostCollectable : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    public bool hasSpeedBoost = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    void Update()
    {
        this.transform.Rotate(0, 0, 1);

        // float
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Speed Boost");
        hasSpeedBoost = true;

        if (other == playerCollider)
        {
            Destroy(this.gameObject);
        }
    }
}
