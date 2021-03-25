using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ARCBALL CAMERA V2
public class CameraScript : MonoBehaviour
{
    // references to the player and the camera target (player head)
    GameObject player;
    GameObject cameraTarget;

    float rotationDirectionX = 0f;
    float rotationDirectionY = 0f;

    // speed the camera will rotate at
    public float rotationSpeed = 1f;

    // camera distance from the player head
    public float radius = 5f;

    // Spherical coords
    Vector3 sc = new Vector3();
    public float minZRotationClamp = -0.4f;
    public float maxZRotationClamp = 0.6f;

    public float smoothingFactor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraTarget = GameObject.Find("cameraTarget");

        this.transform.position = new Vector3(radius, 0.0f, 1.6f);
        this.transform.LookAt(cameraTarget.transform.position);

        sc = GetSphericalCoordinates(this.transform.position);
        sc = new Vector3(5.2f, -1.6f, 0.5f);
        smoothingFactor *= Time.deltaTime;
    }

    // change the cartesian coordinates of the camera to polar
    Vector3 GetSphericalCoordinates(Vector3 cartesian)
    {
        float radius = Mathf.Sqrt(
            Mathf.Pow(cartesian.x, 2) +
            Mathf.Pow(cartesian.y, 2) +
            Mathf.Pow(cartesian.z, 2));

        float phi = Mathf.Atan2(cartesian.z / cartesian.x, cartesian.x);
        float theta = Mathf.Acos(cartesian.y / radius);

        if (cartesian.x < 0) { phi += Mathf.PI; }

        //print(phi);

        return new Vector3(radius, phi, theta);
    }

    // change the polar coordinates to cartesian
    Vector3 GetCartesianCoordinates(Vector3 sphereical)
    {
        Vector3 ret = new Vector3();

        ret.x = sphereical.x * Mathf.Cos(sphereical.z) * Mathf.Cos(sphereical.y);
        ret.y = sphereical.x * Mathf.Sin(sphereical.z);
        ret.z = sphereical.x * Mathf.Cos(sphereical.z) * Mathf.Sin(sphereical.y);

        return ret;
    }

    void InputManager()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationDirectionX = 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationDirectionX = -1;
        }
        else
        {
            rotationDirectionX = 0;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotationDirectionY = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rotationDirectionY = -1;
        }
        else
        {
            rotationDirectionY = 0;
        }
    }

    void SnapCam()
    {
        if (Input.GetKey(KeyCode.Alpha4))
        {
            //sc.y = 0;
            sc.y = Mathf.Lerp(sc.y, 0, smoothingFactor);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            //sc.y = 0.5f * Mathf.PI;
             sc.y = Mathf.Lerp(sc.y, 0.5f * Mathf.PI, smoothingFactor);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            //sc.y = Mathf.PI;
            sc.y = Mathf.Lerp(sc.y, Mathf.PI, smoothingFactor);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            //sc.y = 1.5f * Mathf.PI;
            sc.y = Mathf.Lerp(sc.y, 1.5f * Mathf.PI, smoothingFactor);
        }
    }

    void RotateCam()
    {
        float dx = rotationDirectionX * rotationSpeed;
        float dy = rotationDirectionY * rotationSpeed;

        if (dx != 0f || dy != 0f)
        {
            sc.y += dx * Time.deltaTime;

            //Debug.Log(sc.z);
            sc.z = Mathf.Clamp(sc.z + dy * Time.deltaTime, minZRotationClamp, maxZRotationClamp);
        }
    }

    private void Update()
    {
        InputManager();

        SnapCam();
        RotateCam();

        this.transform.position = GetCartesianCoordinates(sc) + cameraTarget.transform.position;
        this.transform.LookAt(cameraTarget.transform.position);
    }
}

// ARCBALL CAMERA
/*public class CameraScript : MonoBehaviour
{
    // references to the player and the camera target (player head)
    GameObject player;
    GameObject cameraTarget;
    // speed the camera will rotate at
    public float rotationSpeed = 1f;
    // camera distance from the player head
    public float radius = 5f;
    // mouse cursor's position during the last frame
    Vector3 last = new Vector3();
    // Spherical coords
    Vector3 sc = new Vector3();
    // whether the cam should rotate or not
    bool rotateCam = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraTarget = GameObject.Find("cameraTarget");

        this.transform.position = new Vector3(radius, 0.0f, 1.6f);
        this.transform.LookAt(cameraTarget.transform.position);

        sc = GetSphericalCoordinates(this.transform.position);
    }

    // change the cartesian coordinates of the camera to polar
    Vector3 GetSphericalCoordinates(Vector3 cartesian)
    {
        float radius = Mathf.Sqrt(
            Mathf.Pow(cartesian.x, 2) +
            Mathf.Pow(cartesian.y, 2) +
            Mathf.Pow(cartesian.z, 2));

        float phi = Mathf.Atan2(cartesian.z / cartesian.x, cartesian.x);
        float theta = Mathf.Acos(cartesian.y / radius);

        if (cartesian.x < 0) { phi += Mathf.PI; }

        print(phi);

        return new Vector3(radius, phi, theta);
    }

    // change the polar coordinates to cartesian
    Vector3 GetCartesianCoordinates(Vector3 sphereical)
    {
        Vector3 ret = new Vector3();

        ret.x = sphereical.x * Mathf.Cos(sphereical.z) * Mathf.Cos(sphereical.y);
        ret.y = sphereical.x * Mathf.Sin(sphereical.z);
        ret.z = sphereical.x * Mathf.Cos(sphereical.z) * Mathf.Sin(sphereical.y);

        return ret;
    }
    void SnapCam()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            sc.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            sc.y = 0.5f * Mathf.PI;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sc.y = Mathf.PI;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sc.y = 1.5f * Mathf.PI;
        }
    }

    void RotateCam()
    {
        // change over to controller
        // absolute mouse position

        if (Input.GetMouseButtonDown(2))
        {
            last = Input.mousePosition;
            rotateCam = true;
        }
        if (Input.GetMouseButtonUp(2)) { rotateCam = false; }

        if (rotateCam)
        {
            float dx = (last.x - Input.mousePosition.x) * rotationSpeed;
            float dy = (last.y - Input.mousePosition.y) * rotationSpeed;

            if (dx != 0f || dy != 0f)
            {
                sc.y += dx * Time.deltaTime;

                sc.z = Mathf.Clamp(sc.z + dy * Time.deltaTime, -0.5f, 0.5f);

                //this.transform.position = GetCartesianCoordinates(sc) + cameraTarget.transform.position;
                //this.transform.LookAt(cameraTarget.transform.position);
            }

            last = Input.mousePosition;
        }

        this.transform.position = GetCartesianCoordinates(sc) + cameraTarget.transform.position;
        this.transform.LookAt(cameraTarget.transform.position);
    }

    private void Update()
    {
        SnapCam();
        RotateCam();

        Debug.Log(sc);
    }
}*/

// NESW SNAP CAMERA
/*public class CameraScript : MonoBehaviour
{
    // references to the player and the camera target (player head)
    GameObject player;
    GameObject cameraTarget;

    // camera distance from player
    public Vector3 offset;

    // speed the camera will rotate at
    public float rotationSpeed = 1f;
    // direction the camera will rotate
    float rotateDirection = 0;
    public float smoothingFactor = 1f;

    Vector3 prevPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraTarget = GameObject.Find("cameraTarget");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("rotate cam left");
            rotateDirection = -90;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("rotate cam right");
            rotateDirection = 90;
        }
        else
        {
            rotateDirection = 0;
        }

        offset = Quaternion.AngleAxis(rotateDirection * rotationSpeed, Vector3.up) * offset;
        
        this.transform.position = cameraTarget.transform.position + offset;

        //this.transform.position = new Vector3(Mathf.Lerp(prevPos.x, cameraTarget.transform.position.x + offset.x, smoothingFactor * Time.deltaTime),
        //    Mathf.Lerp(prevPos.y, cameraTarget.transform.position.y + offset.y, smoothingFactor * Time.deltaTime),
        //    Mathf.Lerp(prevPos.z, cameraTarget.transform.position.z + offset.z, smoothingFactor * Time.deltaTime));

        this.transform.LookAt(cameraTarget.transform.position);
    }

    private void LateUpdate()
    {
        prevPos = this.transform.position;
    }
}*/

/*public class CameraScript : MonoBehaviour
{
    GameObject player;
    GameObject cameraTarget;

    Vector3 offset;

    float mouseX;
    float mouseY;

    // speed the camera will rotate at
    public float rotationSpeed = 1f;
    // direction the camera will rotate
    float rotateDirection = 0;
    public float smoothingFactor = 1f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraTarget = GameObject.Find("cameraTarget");
        
        offset = this.transform.position - cameraTarget.transform.position;
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            // North
            this.transform.rotation =  Quaternion.Euler(0, 0 ,0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // East
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // South
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // West
            this.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            rotateDirection = 0;
            mouseX = Input.GetAxis("Mouse X");
            offset = Quaternion.Euler(0, mouseX, 0) * offset;

            float desAngle = cameraTarget.transform.eulerAngles.y;
            Quaternion rot = Quaternion.Euler(0, desAngle, 0);

            //Vector3 desPos = cameraTarget.transform.position + offset;

            this.transform.position = cameraTarget.transform.position + (rot * offset);
            this.transform.LookAt(cameraTarget.transform.position);
        }

        this.transform.position = cameraTarget.transform.position + offset;
        // this.transform.LookAt(cameraTarget.transform.position);
    }
}*/

/*public class CameraScript : MonoBehaviour
{
    GameObject player;
    GameObject cameraTarget;

    public float CameraYOffset = 0f;
    public float CameraZOffset = 0f;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraTarget = GameObject.Find("cameraTarget"); 
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(cameraTarget.transform.position);
        this.transform.position = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y + CameraYOffset, cameraTarget.transform.position.z - CameraZOffset);

        //changeCameraAxis();
    }

    void RotateCamera()
    {

    }

    void changeCameraAxis()
    {
        this.transform.LookAt(cameraTarget.transform.position);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("North");
            //this.transform.rotation = Quaternion.Euler(0,0,0);
            this.transform.position = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y + CameraYOffset, cameraTarget.transform.position.z - CameraZOffset);
            this.transform.forward = new Vector3(0, 0, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("East");
            //this.transform.rotation = Quaternion.Euler(0, 90, 0);
            this.transform.forward = new Vector3(1.0f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("South");
            //this.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.transform.position = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y + CameraYOffset, cameraTarget.transform.position.z +  CameraZOffset);
            this.transform.forward = new Vector3(0, 0, -1.0f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("West");
            //this.transform.rotation = Quaternion.Euler(0, 270, 0);
            this.transform.forward = new Vector3(-1.0f, 0, 0);
        }
    }
}*/

/*public class CameraScript : MonoBehaviour
{
    GameObject cameraTarget;

    public float rotationSpeed = 1f;
    float mouseX;
    float mouseY;

    void Start()
    {
        cameraTarget = GameObject.Find("cameraTarget");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        this.transform.LookAt(cameraTarget.transform.position);

        cameraTarget.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }


}*/

/*public class CameraScript : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    Transform player;

    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(player.position.x, player.position.y + 1f, player.position.z + 1f);
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}*/