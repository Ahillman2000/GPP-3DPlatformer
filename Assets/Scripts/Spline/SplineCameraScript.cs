using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCameraScript : MonoBehaviour
{
    SplineObjectParenting parentingScript;
    SplineScript splineScript;

    GameObject platform;
    GameObject mainCamera;
    GameObject splineCamera;
    public Vector3 offset;

    public GameObject cameraTarget;

    // Start is called before the first frame update
    void Start()
    {
        splineCamera = GameObject.Find("Spline Camera");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        platform = GameObject.Find("Spline Platform");
        splineScript = GameObject.Find("Spline").GetComponent<SplineScript>();

        parentingScript = platform.GetComponent<SplineObjectParenting>();

        splineCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(parentingScript.playerOnPlatform && platform.transform.position != splineScript.splinePoint[splineScript.splineCount].transform.position)
        {
            splineCamera.SetActive(true);
            mainCamera.SetActive(false);
        }
        else
        {
            mainCamera.SetActive(true);
            splineCamera.SetActive(false);
        }

        //splineCamera.transform.position = platform.transform.position + offset;
        splineCamera.transform.LookAt(cameraTarget.transform.position);
    }
}
