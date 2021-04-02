using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public GameObject platformSwitch;

    public GameObject[] points;
    //public GameObject[] platforms;
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

        if (platform.transform.position == points[nextPoint].transform.position)
        {
            nextPoint += 1;
        }
        if (platform.transform.position == points[maxPointIndex].transform.position)
        {
            platform.transform.position = points[0].transform.position;

            // destory current platform
            // instantiate new platform
            nextPoint = 1;
        }

        platform.transform.position = Vector3.MoveTowards(platform.transform.position, points[nextPoint].transform.position, step);

    }
}
