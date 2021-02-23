using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContollerScript : MonoBehaviour
{
    private Animator anim;
    private CharacterController _controller;
    private float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        _controller = this.GetComponent<CharacterController>();
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
            speed = 15f;
        }

        _controller.Move(move * Time.deltaTime * speed);
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
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
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
