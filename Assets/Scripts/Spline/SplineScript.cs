using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineScript : MonoBehaviour
{
    SplineObjectParenting splineObjectParentingScript;

    public GameObject[] splinePoint;
    public GameObject splineObject;

    GameObject player;

    public bool drawSpline = true;

    public int splineCount;
    int destinationSplinePoint = 1;
    public float speed = 1;

    Vector3 currentVector;
    Vector3 lastVector;
    Vector3 directionalVector;

    enum TravelDirectionEnum {North, East, South, West};
    TravelDirectionEnum travelDirectionEnum;
    
    void Start()
    {
        splineObjectParentingScript = splineObject.GetComponent<SplineObjectParenting>();
        player = GameObject.FindGameObjectWithTag("Player");

        splineCount = splinePoint.Length - 1;
        //Debug.Log(splineCount);

        //Debug.Log("object placed at beginning");
        splineObject.transform.position = splinePoint[0].transform.position;
    }

    void FixedUpdate()
    {
        if (splineCount > 1)
        {
            for (int i = 0; i < splineCount; i++)
            {
                Debug.DrawLine(splinePoint[i].transform.position, splinePoint[i + 1].transform.position, Color.green);
            }
            //Debug.DrawLine(splinePoint[splineCount].transform.position, splinePoint[0].transform.position, Color.green);
        }

        if (splineObjectParentingScript.playerOnPlatform)
        {
            if (splineObject.transform.position == splinePoint[destinationSplinePoint].transform.position)
            {
                //Debug.Log("next spline point");
                destinationSplinePoint += 1;
            }
            if (splineObject.transform.position == splinePoint[splineCount].transform.position)
            {
                //Debug.Log("returning to start");
                destinationSplinePoint = splineCount;
            }

            //Debug.Log("travelling to destination spline point");
            splineObject.transform.position = Vector3.MoveTowards(splineObject.transform.position, splinePoint[destinationSplinePoint].transform.position, speed * Time.deltaTime);
            
            currentVector = splineObject.transform.position;
            directionalVector = currentVector - lastVector;

            DetermineTravelDirection();
            RotateSplineObject();
        }
    }

    private void LateUpdate()
    {
        lastVector = splineObject.transform.position;
    }

    void DetermineTravelDirection()
    {
        if (directionalVector.x > 0)
        {
            travelDirectionEnum = TravelDirectionEnum.East;
        }
        else if (directionalVector.x < 0)
        {
            travelDirectionEnum = TravelDirectionEnum.West;
        }
        else if (directionalVector.z > 0)
        {
            travelDirectionEnum = TravelDirectionEnum.North;
        }
        else if (directionalVector.z < 0)
        {
            travelDirectionEnum = TravelDirectionEnum.South;
        }
    }

    void RotateSplineObject()
    {
        if (travelDirectionEnum == TravelDirectionEnum.East)
        {
            splineObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (travelDirectionEnum == TravelDirectionEnum.West)
        {
            splineObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (travelDirectionEnum == TravelDirectionEnum.North)
        {
            splineObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (travelDirectionEnum == TravelDirectionEnum.South)
        {
            splineObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
