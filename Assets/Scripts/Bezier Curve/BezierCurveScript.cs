using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveScript : MonoBehaviour
{
    public Transform[] controlPoints;
    Vector3 gizmosPosition;

    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        for (float i = 0; i < 1; i += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - i, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - i, 2) * i * controlPoints[1].position + 3 * (1 - i) * Mathf.Pow(i, 2) * controlPoints[2].position + 
                Mathf.Pow(i, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
        Gizmos.DrawLine(controlPoints[2].position, controlPoints[3].position);
    }

    void Update()
    {
        
    }
}
