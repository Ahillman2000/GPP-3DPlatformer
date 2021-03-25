using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Cutscene : MonoBehaviour
{
    // Cutscene objects
    DoorController doorControllerScript;

    GameObject player;
    GameObject cutsceneCharacter;

    GameObject button;

    Animator anim;

    public GameObject playerCam;
    public GameObject cutsceneCam;

    // Movable objects
    public GameObject position1;
    public GameObject position2;

    public int speed;
    public bool end;

    bool punch;

    private void Start()
    {
        doorControllerScript = GameObject.Find("button1").GetComponent<DoorController>();
        player = GameObject.FindGameObjectWithTag("Player");
        cutsceneCharacter = GameObject.Find("cutsceneCharacter");
        anim = cutsceneCharacter.GetComponent<Animator>();
        button = GameObject.Find("button1");

        cutsceneCharacter.SetActive(false);
        cutsceneCam.transform.position = position1.transform.position;

        punch = true;
    }
    void Hit()
    {
        if (punch)
        {
            Debug.Log("punch");
            anim.SetBool("punch", true);
            punch = false;
        }

        else 
        {
            anim.SetBool("punch", false); 
        }
    }

    private void FixedUpdate()
    {
        float step = speed * Time.deltaTime;

        if (doorControllerScript.start_cutscene)
        {
            //doorControllerScript.start_cutscene = false;
            player.SetActive(false);
            cutsceneCharacter.SetActive(true);
            cutsceneCam.SetActive(true);
            playerCam.SetActive(false);

            Hit();
            button.transform.position = new Vector3(-19.52f, 3f, 10.394f);

            // cutscene cam not moving
            cutsceneCam.transform.position = Vector3.MoveTowards(cutsceneCam.transform.position, position2.transform.position, step);

            if (cutsceneCam.transform.position == position2.transform.position)
            {
                doorControllerScript.isOpening = true;
            }

            if (doorControllerScript.stoppedOpening)
            {
                StartCoroutine(EndCutscene());
            }
        }
    }

    IEnumerator EndCutscene()
    {
        yield return new WaitForSeconds(2);

        end = true;
        doorControllerScript.start_cutscene = false;
        cutsceneCharacter.SetActive(false);
        player.SetActive(true);
        playerCam.SetActive(true);
        cutsceneCam.SetActive(false);
    }
}*/

// rather specific naming conventions
public class CutsceneScript : MonoBehaviour
{
    ButtonScript buttonScript;
    DoorController doorControllerScript;

    GameObject player;
    GameObject cutsceneCharacter;
    GameObject button;

    public GameObject playerCam;
    public GameObject cutsceneCam;

    // Movable objects
    public GameObject position1;
    public GameObject position2;

    Animator anim;

    public int speed;

    enum CutsceneState {none, punching, movingCamera, lookingAtDoor }
    CutsceneState cutsceneState;

    public bool openDoor = false;

    private void Start()
    {
        doorControllerScript = GameObject.Find("door").GetComponent<DoorController>();
        buttonScript = GameObject.Find("button").GetComponent<ButtonScript>();

        player = GameObject.FindGameObjectWithTag("Player");
        cutsceneCharacter = GameObject.Find("cutsceneCharacter");
        anim = cutsceneCharacter.GetComponent<Animator>();
        button = GameObject.Find("button1");

        cutsceneCharacter.SetActive(false);
        cutsceneCam.transform.position = position1.transform.position;

        cutsceneState = CutsceneState.none;
    }

    private void FixedUpdate()
    {
        float step = speed * Time.deltaTime;

        if (buttonScript.near_button == true && Input.GetMouseButtonDown(0))
        {
            cutsceneState = CutsceneState.punching;
            print("cutscene running punch");
        }

        else if (cutsceneState == CutsceneState.punching)
        {
            // play animation
            // moving camera state
            Debug.Log("punch");
            anim.SetBool("punch", true);
            cutsceneState = CutsceneState.movingCamera;
        }
        else if (cutsceneState == CutsceneState.movingCamera)
        {
            anim.SetBool("punch", false);
            // move cam

            if (cutsceneCam.transform.position == position2.transform.position)
            {
                cutsceneState = CutsceneState.lookingAtDoor;
            }
            else
            {
                cutsceneCam.transform.position = Vector3.MoveTowards(cutsceneCam.transform.position, position2.transform.position, step);
            }
        }
        else if (cutsceneState == CutsceneState.lookingAtDoor)
        {
            // wait 2 seconds
            openDoor = true;
        }
    }
}