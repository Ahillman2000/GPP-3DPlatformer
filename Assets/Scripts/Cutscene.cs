using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    doorController doorControllerScript;

    GameObject player;
    GameObject cutsceneCharacter;

    public GameObject playerCam;
    public GameObject cutsceneCam;

    private void Start()
    {
        doorControllerScript = GameObject.Find("button1").GetComponent<doorController>();
        player = GameObject.FindGameObjectWithTag("Player");
        cutsceneCharacter = GameObject.Find("cutsceneCharacter");

        cutsceneCharacter.SetActive(false);
    }

    private void Update()
    {
        if (doorControllerScript.cutscene)
        {
            doorControllerScript.cutscene = false;
            player.SetActive(false);
            cutsceneCharacter.SetActive(true);
            cutsceneCam.SetActive(true);
            playerCam.SetActive(false);




            StartCoroutine(FinishCut());
        }
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(2);
        cutsceneCharacter.SetActive(false);
        player.SetActive(true);
        playerCam.SetActive(true);
        cutsceneCam.SetActive(false);
    }
}
