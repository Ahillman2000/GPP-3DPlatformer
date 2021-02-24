using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCollectable : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    public bool hasDoubleJump = false;

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
        //Debug.Log("Double Jump");
        hasDoubleJump = true;

        if (other == playerCollider)
        {
            Destroy(this.gameObject);
        }
    }
}
