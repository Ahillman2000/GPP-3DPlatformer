using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostCollectable : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    public bool hasSpeedBoost = false;

    GameObject effect;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();

        effect = GameObject.Find("Speed Boost particle system");
        effect.SetActive(false);
    }

    void Update()
    {
        this.transform.Rotate(0, 0, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        effect.SetActive(true);
        //Debug.Log("Speed Boost collected");
        hasSpeedBoost = true;

        if (other == playerCollider)
        {
            Destroy(this.gameObject);
        }
    }
}
