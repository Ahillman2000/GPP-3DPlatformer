using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContollerScript : MonoBehaviour
{
    private Animator anim;
    private CharacterController _controller;
    private float speed = 0;
    private Vector3 _velocity = new Vector3(0,0,0);
    private float JumpHeight = 1;

    public GameObject speedBoostPowerup;
    public GameObject doubleJumpPowerup;
    private SpeedBoostCollectable speedBoostCollectable;
    private DoubleJumpCollectable doubleJumpCollectable;

    private float speedBoostTimer = 0.0f;
    int seconds;

    int jumpCounter = 0;

    TrailRenderer speedLines;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        _controller = this.GetComponent<CharacterController>();

        speedBoostCollectable = speedBoostPowerup.GetComponent<SpeedBoostCollectable>();
        doubleJumpCollectable = doubleJumpPowerup.GetComponent<DoubleJumpCollectable>();
        speedLines = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            speed = 7.5f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (speedBoostCollectable.hasSpeedBoost && seconds <= 10)
            {
                Debug.Log("speed boost run");
                speed = 30;
                speedLines.enabled = true;
            }
            else
            {
                Debug.Log("regular run");
                speed = 15f;
                speedLines.enabled = false;
            }
        }

        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
        else
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
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


        //if (/*grounded && */ !Input.GetKeyDown(KeyCode.Space))
        //{
        // anim.SetBool("Jump", false);
        //}
        if (Input.GetKey(KeyCode.Space) && (_controller.isGrounded || doubleJumpCollectable.hasDoubleJump))
        {
            _velocity.y = 0;
            _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
            anim.SetBool("Jump", true);
            jumpCounter++;
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        if (speedBoostCollectable.hasSpeedBoost)
        {
            speedBoostTimer += Time.deltaTime;
            seconds = (int)(speedBoostTimer % 60);
            //Debug.Log(seconds);

            /*if (seconds <= 10)
            {
                Debug.Log(seconds + " speed boost available");
                speedLines.enabled = true;
            }
            else
            {
                Debug.Log("speed boost not available");
                speedLines.enabled = false;
            }*/
        }
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
}
