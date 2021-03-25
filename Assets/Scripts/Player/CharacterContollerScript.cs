using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContollerScript : MonoBehaviour
{
    private Animator anim;
    private CharacterController _controller;
    SplineScript splineScript;
    SplineObjectParenting splineObjectParentingScript;
    public GameObject platform;

    Vector3 move;
    private float speed = 0;
    private Vector3 _velocity = new Vector3(0,0,0);
    private float JumpHeight = 1;

    public GameObject speedBoostPowerup;
    public GameObject doubleJumpPowerup;
    private SpeedBoostCollectable speedBoostCollectable;
    private DoubleJumpCollectable doubleJumpCollectable;

    private float speedBoostTimer = 0.0f;
    int seconds;

    //TrailRenderer speedLines;

    int jumpCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        _controller = this.GetComponent<CharacterController>();
        splineScript = GameObject.Find("Spline").GetComponent<SplineScript>();
        splineObjectParentingScript = platform.GetComponent<SplineObjectParenting>();

        speedBoostCollectable = speedBoostPowerup.GetComponent<SpeedBoostCollectable>();
        doubleJumpCollectable = doubleJumpPowerup.GetComponent<DoubleJumpCollectable>();
        //speedLines = GetComponent<TrailRenderer>();
    }

    void Run()
    {
        if (splineObjectParentingScript.playerOnPlatform && platform.transform.position != splineScript.splinePoint[splineScript.splineCount].transform.position)
        {
            move = new Vector3(0, 0, 0);
        }
        else
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            speed = 7.5f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (speedBoostCollectable.hasSpeedBoost && seconds <= 10)
            {
                //Debug.Log("speed boost run");
                speed = 30;
                //speedLines.enabled = true;
            }
            else
            {
                //Debug.Log("regular run");
                speed = 15f;
                //speedLines.enabled = false;
            }
        }

        _controller.Move(move * Time.deltaTime * speed);
        _controller.Move(_velocity * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.forward = move;
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (_controller.isGrounded || doubleJumpCollectable.hasDoubleJump))
        {
            if (jumpCounter < 2)
            {
                //anim.SetBool("Jump", true);
                _velocity.y = 0;
                _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
                jumpCounter++;
            }
        }
        if (_controller.isGrounded)
        {
            jumpCounter = 0;
        }
    }
    void Punch()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("punch", true);
        }
        else
        {
            anim.SetBool("punch", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speedBoostCollectable.hasSpeedBoost)
        {
            speedBoostTimer += Time.deltaTime;
            seconds = (int)(speedBoostTimer % 60);
        }

        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
        else
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        Run();
        Jump();
        Punch();
    }

    void Land()
    {

    }
    void FootL()
    {

    }
    void FootR()
    {

    }
    void Hit()
    {
        
    }
}
