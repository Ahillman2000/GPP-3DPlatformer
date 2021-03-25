using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool near_button = false;

    GameObject player;
    Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            near_button = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            near_button = false;
        }
    }
}
