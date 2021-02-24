using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    GameObject player;
    public GameObject cameraTarget;

    public float playerCameraYOffset = 5f;
    public float playerCameraZOffset = 250f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + playerCameraYOffset, player.transform.position.z - playerCameraZOffset);
        this.transform.LookAt(cameraTarget.transform.position);
    }
}
