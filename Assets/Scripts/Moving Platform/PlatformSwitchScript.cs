using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitchScript : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;
    GameObject lever;

    enum SwitchState { off, on };
    SwitchState switchState;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
        lever = GameObject.Find("Switch pivot");
        switchState = SwitchState.off;
    }

    void Update()
    {
        if (switchState == SwitchState.off)
        {
            // lerp switch lever x rotation to -45
            //Mathf.Lerp(lever.transform.rotation.x, -45f, 0.1f * Time.deltaTime);
            lever.transform.rotation = Quaternion.Euler(-45f, 0, 0);
        }
        else if (switchState == SwitchState.on)
        {
            // lerp switch pivot x rotation to -135
            //Mathf.Lerp(lever.transform.rotation.x, -135f, 0.1f * Time.deltaTime);
            lever.transform.rotation = Quaternion.Euler(-135f, 0, 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == playerCollider && Input.GetKeyDown(KeyCode.E))
        {
            if(switchState == SwitchState.off)
            {
                switchState = SwitchState.on;
                Debug.Log("on");
            }
            else if (switchState == SwitchState.on)
            {
                switchState = SwitchState.off;
                Debug.Log("off");
            }
        }
    }
}
