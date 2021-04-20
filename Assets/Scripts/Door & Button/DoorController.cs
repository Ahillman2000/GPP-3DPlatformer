using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class DoorController : MonoBehaviour
{
    Cutscene cutscene;

    GameObject player;
    Collider playerCollider;

    public GameObject door;

    bool near_button = false;
    public bool isOpening;
    public bool stoppedOpening;

    public bool shut_door_on_timer;

    public bool start_cutscene;

    public float door_speed = 1f;

    // enum closed, opening, open, closing
    // state handles the actions wihtin itself
    // state machine and messages
    // transform to handle finsih vector

    void Start()
    {
        cutscene = GameObject.Find("SceneManager").GetComponent<Cutscene>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(near_button && Input.GetMouseButtonDown(0) && door.transform.position.y <= 3f)
        {
            start_cutscene = true;
        }
    }

    private void FixedUpdate()
    {
        if (isOpening)
        {
            door.transform.Translate(Vector3.up * Time.deltaTime * door_speed);
        }

        if (door.transform.position.y >= 3.5f)
        {
            isOpening = false;
            stoppedOpening = true;
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
}*/

// rename scripts
public class DoorController : MonoBehaviour
{
    CutsceneScript cutsceneScript;
    ButtonScript buttonScript;

    public GameObject buttonObject;

    GameObject doorFinishPosition;

    enum DoorState { closed, opening, open, closing }
    DoorState doorState;
    public float door_speed = 1f;
    Vector3 doorStartingPosition;
    public float doorWaitTime = 2.0f;

    float doorTimer;
    int seconds;

    void Start()
    {
        cutsceneScript = GameObject.Find("Scene Manager").GetComponent<CutsceneScript>();
        buttonScript = buttonObject.GetComponent<ButtonScript>();

        doorState = DoorState.closed;
        doorStartingPosition = this.transform.position;
        doorFinishPosition = GameObject.Find("doorFinishPosition");
    }

    private void FixedUpdate()
    {
        float step = door_speed * Time.deltaTime;

        if (doorState == DoorState.closed)
        {
            if (cutsceneScript.openDoor)
            {
                Debug.Log("Door Opening");
                doorState = DoorState.opening;
            }
        }
        else if (doorState == DoorState.opening)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, doorFinishPosition.transform.position, step);

            if(this.transform.position.y >= doorFinishPosition.transform.position.y)
            {
                Debug.Log("Door Open");
                doorState = DoorState.open;
            }
        }

        else if (doorState == DoorState.open)
        {
            doorTimer += Time.deltaTime;
            seconds = (int)(doorTimer % 60);
            if(seconds == doorWaitTime)
            {
                Debug.Log("Door Closing");
                doorState = DoorState.closing;
            }
        }

        else if (doorState == DoorState.closing)
        {
            doorTimer = 0.0f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, doorStartingPosition, step);

            if (this.transform.position.y <= doorStartingPosition.y)
            {
                Debug.Log("Door Closed");
                doorState = DoorState.closed;
            }
        }
    }
}