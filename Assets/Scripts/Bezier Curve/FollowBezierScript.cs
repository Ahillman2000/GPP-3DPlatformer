using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBezierScript : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;

    public Transform startPositon;

    public Transform[] curves;
    int nextCurveToFollow;

    float t;

    public float speed = 0.1f;

    bool coroutineAllowed = false;

    public bool repeat;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();

        this.transform.position = startPositon.position;

        nextCurveToFollow = 0;
        t = 0;
        //coroutineAllowed = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other == playerCollider)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                coroutineAllowed = true;
            }
        }
    }

    void Update()
    {
        if (coroutineAllowed){StartCoroutine(FollowCurve(nextCurveToFollow));}
    }

    IEnumerator FollowCurve(int curveNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = curves[curveNumber].GetChild(0).position;
        Vector3 p1 = curves[curveNumber].GetChild(1).position;
        Vector3 p2 = curves[curveNumber].GetChild(2).position;
        Vector3 p3 = curves[curveNumber].GetChild(3).position;

        while(t < 1)
        {
            t += Time.deltaTime * speed;

            this.transform.position = Mathf.Pow(1 - t, 3) * p0 +
                3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                Mathf.Pow(t, 3) * p3;

            yield return new WaitForEndOfFrame();
        }

        t = 0f;
        nextCurveToFollow += 1;

        if(nextCurveToFollow > curves.Length - 1)
        {
            coroutineAllowed = false;
            nextCurveToFollow = 0; 
        }
        else
        {
            coroutineAllowed = true;
        }
    }
}
