using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    public GameObject door;

    bool near_button = false;
    bool isOpening;
    bool isHolding;
    bool isClosing;
    float door_timer = 0;

    public bool shut_door_on_timer;

    bool showCutscene = true;
    public bool cutscene;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    void ShutDoor()
    {
        if (isHolding)
        {
            // count to 5 seconds
            // isHolding = false;
            // isClosing = true;
            door_timer += Time.deltaTime;
            int seconds = (int)(door_timer % 60);
            //print(seconds);

            if (seconds >= 5)
            {
                door_timer = 0;
                isHolding = false;
                isClosing = true;
            }
        }
        if (isClosing)
        {
            door.transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
        if (door.transform.position.y <= 2.5f)
        {
            isClosing = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(near_button && Input.GetMouseButtonDown(0) && door.transform.position.y < 5.5f)
        {
            //print("player has pressed button");
            //transform.Translate(Vector3.back * Time.deltaTime * 1);

            if(showCutscene)
            {
                cutscene = true;
                showCutscene = false;
            }

            isOpening = true;
        }
        if(isOpening)
        {
            door.transform.Translate(Vector3.up * Time.deltaTime * 5);
        }

        if (door.transform.position.y >= 5.5f)
        {
            isOpening = false;
            isHolding = true;
        }
        
        if(shut_door_on_timer)
        {
            ShutDoor();
        }
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
