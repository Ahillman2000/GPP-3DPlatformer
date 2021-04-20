using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    GameObject player;

    public GameObject platformSwitch;
    PlatformSwitchScript platformSwitchScript;

    public GameObject[] points;
    //public GameObject[] platforms;

    GameObject previousPlatform;
    public GameObject platform;
    public float platformSpeed = 1;
    private float step;

    int maxPointIndex;
    //int maxPlatformIndex;

    int nextPoint = 1;

    public bool drawConnections = true;

    // Start is called before the first frame update
    void Start()
    {
        platformSwitchScript = platformSwitch.GetComponent<PlatformSwitchScript>();

        player = GameObject.FindGameObjectWithTag("Player");

        platform.transform.position = points[0].transform.position;
        maxPointIndex = points.Length - 1;
        //maxPlatformIndex = points.Length - 1;
    }
    
    void DrawDebugLines()
    {
        if (points.Length >= 2)
        {
            for (int i = 0; i < maxPointIndex; i++)
            {
                Debug.DrawLine(points[i].transform.position, points[i + 1].transform.position, Color.green);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(drawConnections)
        {
            DrawDebugLines();
        }

        step = platformSpeed * Time.deltaTime;

        // if a platform exists
        if(platform != null && platformSwitchScript.switchOn == true)
        {
            if (platform.transform.position == points[nextPoint].transform.position)
            {
                nextPoint += 1;
            }
            if (platform.transform.position == points[maxPointIndex].transform.position)
            {
                platform.transform.position = points[0].transform.position;

                previousPlatform = platform;

                // deparent player before destroying
                player.transform.parent = null;
                // destory current platform
                GameObject.Destroy(platform);

                // instantiate new platform
                platform = Instantiate(platform, points[0].transform.position, previousPlatform.transform.rotation);

                nextPoint = 1;
            }

            platform.transform.position = Vector3.MoveTowards(platform.transform.position, points[nextPoint].transform.position, step);
        }
    }
}
