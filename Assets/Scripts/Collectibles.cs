using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, 359);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touch");

        if (other == playerCollider)
        {
            Destroy(this.gameObject);
        }
    }
}
