﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCollectable : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    public bool hasDoubleJump = false;

    GameObject effect;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();

        effect = GameObject.Find("Double Jump particle system");
        effect.SetActive(false);
    }

    void Update()
    {
        this.transform.Rotate(0, 0, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        effect.SetActive(true);
        //Debug.Log("Double Jump collected");
        hasDoubleJump = true;

        if (other == playerCollider)
        {
            Destroy(this.gameObject);
        }
    }
}
