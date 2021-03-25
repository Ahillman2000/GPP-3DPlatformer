using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject character;
    Animator anim;

    public float player_x_movement_velocity;
    public float player_z_movement_velocity;

    public float player_y_rotation_velocity;

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        anim = character.GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void PlayerControls()
    {
        // walk
        if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            // forward
            if (Input.GetKey("w"))
            {
                character.transform.forward = new Vector3(0, 0, 1);
            }
            // left
            if (Input.GetKey("a"))
            {
                character.transform.forward = new Vector3(-1, 0, 0);
            }
            // back
            if (Input.GetKey("s"))
            {
                character.transform.forward = new Vector3(0, 0, -1);
            }
            // right
            if (Input.GetKey("d"))
            {
                character.transform.forward = new Vector3(1, 0, 0);
            }

            anim.SetBool("Walk", true);
            Walk();
        }

        // run
        else if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            // forward
            if (Input.GetKey("w"))
            {
                character.transform.forward = new Vector3(0, 0, 1);
            }
            // left
            if (Input.GetKey("a"))
            {
                character.transform.forward = new Vector3(-1, 0, 0);
            }
            // back
            if (Input.GetKey("s"))
            {
                character.transform.forward = new Vector3(0, 0, -1);
            }
            // right
            if (Input.GetKey("d"))
            {
                character.transform.forward = new Vector3(1, 0, 0);
            }

            anim.SetBool("Run", true);
            Run();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
        }

        else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Run",  false);
        }
    }

    void Walk()
    {
        character.transform.Translate(0, 0, player_z_movement_velocity);
    }
    void Run()
    {
        character.transform.Translate(0, 0, player_z_movement_velocity * 2);
    }

    void Update()
    {
        PlayerControls();
    }
}
