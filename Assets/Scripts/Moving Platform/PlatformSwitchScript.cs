using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitchScript : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;
    GameObject lever;

    //enum SwitchState { off, on };
    //SwitchState switchState;

    public bool switchOn = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
        lever = GameObject.Find("Switch pivot");
        //switchState = SwitchState.off;
    }

    void Update()
    {
        /*if (switchState == SwitchState.off)
        {
            lever.transform.rotation = Quaternion.Euler(-45f, 0, 0);
        }
        else if (switchState == SwitchState.on)
        {
            lever.transform.rotation = Quaternion.Euler(-135f, 0, 0);
        }*/

        if (!switchOn)
        {
            lever.transform.rotation = Quaternion.Euler(-45f, 0, 0);
        }
        else
        {
            lever.transform.rotation = Quaternion.Euler(-135f, 0, 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == playerCollider && Input.GetKeyDown(KeyCode.E))
        {
            /*if(switchState == SwitchState.off)
            {
                switchState = SwitchState.on;
                Debug.Log("on");
            }
            else if (switchState == SwitchState.on)
            {
                switchState = SwitchState.off;
                Debug.Log("off");
            }*/

            if (!switchOn)
            {
                switchOn = true;
            }
            else if (switchOn)
            {
                switchOn = false;
            }
        }
    }
}
